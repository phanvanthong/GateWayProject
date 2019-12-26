using AutoMapper;
using GetWay.API.Models;
using GetWay.Common.Constants;
using GetWay.Common.Models;
using GetWay.Data.Dto;
using GetWay.Data.Models;
using GetWay.Extension.Extensions;
using GetWay.Service.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace GetWay.API.Controllers.Dashboard
{
    public class ManagerPlanController : ApiController
    {
        private readonly IPlanService _planservice;

        private readonly IMapper _mapper;

        public ManagerPlanController(IPlanService planservice, IMapper mapper)
        {
            this._planservice = planservice;
            this._mapper = mapper;
        }

        #region methods

        [Route("api/Plans/all")]
        public IHttpActionResult GetPlans()
        {
            ResponseDataDTO<IEnumerable<Plan>> response = new ResponseDataDTO<IEnumerable<Plan>>();
            try
            {
                response.Code = HttpCode.OK;
                response.Message = MessageResponse.SUCCESS;
                response.Data = _planservice.GetAll();
            }
            catch (Exception ex)
            {
                response.Code = HttpCode.INTERNAL_SERVER_ERROR;
                response.Message = MessageResponse.FAIL;
                response.Data = null;

                Console.WriteLine(ex.ToString());
            }

            return Ok(response);
        }

        [Route("api/Plans/{garageID}")]
        public IHttpActionResult GetPlans(Guid planID)
        {
            ResponseDataDTO<Plan> response = new ResponseDataDTO<Plan>();
            try
            {
                response.Code = HttpCode.OK;
                response.Message = MessageResponse.SUCCESS;
                response.Data = _planservice.GetPlanByID(planID);
            }
            catch (Exception ex)
            {
                response.Code = HttpCode.INTERNAL_SERVER_ERROR;
                response.Message = MessageResponse.FAIL;
                response.Data = null;

                Console.WriteLine(ex.ToString());
            }

            return Ok(response);
        }

        [Route("api/Plans/page")]
        public IHttpActionResult GetPlansPaging(int pageSize, int pageNumber)
        {
            ResponseDataDTO<PagedResults<Plan>> response = new ResponseDataDTO<PagedResults<Plan>>();
            try
            {
                response.Code = HttpCode.OK;
                response.Message = MessageResponse.SUCCESS;
                response.Data = _planservice.CreatePagedResults(pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                response.Code = HttpCode.INTERNAL_SERVER_ERROR;
                response.Message = MessageResponse.FAIL;
                response.Data = null;

                Console.WriteLine(ex.ToString());
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("api/Plans/create")]
        public async Task<IHttpActionResult> CreatePlan()
        {
            ResponseDataDTO<int> response = new ResponseDataDTO<int>();
            try
            {
                var path = Path.GetTempPath();
                MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(path);
                // get data from formdata
                PlanViewModel planViewModel = new PlanViewModel
                {
                    PlanID = Guid.Parse(streamProvider.FormData["PlanID"])
                };
                // mapping view model to entity
                var createdPlan = _mapper.Map<Plan>(planViewModel);
                // save new Plan
                _planservice.Create(createdPlan);
                // return response
                response.Code = HttpCode.OK;
                response.Message = MessageResponse.SUCCESS;
                response.Data = 1;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Code = HttpCode.INTERNAL_SERVER_ERROR;
                response.Message = MessageResponse.FAIL;
                response.Data = 0;
                Console.WriteLine(ex.ToString());

                return Ok(response);
            }

        }


        [HttpPut]
        [Route("api/Plans/update")]
        public async Task<IHttpActionResult> UpdatePlan()
        {
            ResponseDataDTO<int> response = new ResponseDataDTO<int>();
            try
            {
                var path = Path.GetTempPath();

                MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(path);

                // get data from formdata
                PlanViewModel planViewModel = new PlanViewModel
                {
                    CarID = Guid.Parse(streamProvider.FormData["CarID"]),
                    PlanID = Guid.Parse(streamProvider.FormData["PlanID"]),
                    DriverID = Guid.Parse(streamProvider.FormData["DriverID"]),
                    TeacherID = Guid.Parse(streamProvider.FormData["TeacherID"]),
                };
                var existPlan = _planservice.Find(planViewModel.PlanID);
                
                // mapping view model to entity
                var updatedPlan = _mapper.Map<Plan>(planViewModel);

                // update Plan
                _planservice.Update(updatedPlan, updatedPlan.PlanID);
                // return response
                response.Code = HttpCode.OK;
                response.Message = MessageResponse.SUCCESS;
                response.Data = 1;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Code = HttpCode.INTERNAL_SERVER_ERROR;
                response.Message = MessageResponse.FAIL;
                response.Data = 0;
                Console.WriteLine(ex.ToString());

                return Ok(response);
            }
        }

        [HttpDelete]
        [Route("api/Plans/delete/{PlanId}")]
        public IHttpActionResult DeletePlan(int PlanId)
        {
            ResponseDataDTO<int> response = new ResponseDataDTO<int>();
            try
            {
                var PlanDeleted = _planservice.Find(PlanId);
                if (PlanDeleted != null)
                {
                    _planservice.Delete(PlanDeleted);

                    // return response
                    response.Code = HttpCode.OK;
                    response.Message = MessageResponse.SUCCESS;
                    response.Data = 1;
                    return Ok(response);
                }
                else
                {
                    // return response
                    response.Code = HttpCode.NOT_FOUND;
                    response.Message = MessageResponse.FAIL;
                    response.Data = 0;

                    return Ok(response);
                }


            }
            catch (Exception ex)
            {
                response.Code = HttpCode.INTERNAL_SERVER_ERROR;
                response.Message = MessageResponse.FAIL;
                response.Data = 0;
                Console.WriteLine(ex.ToString());

                return Ok(response);
            }
        }

        #endregion

        #region dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _planservice.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
