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
    public class ManagerGarageController : ApiController
    {
        private readonly IGarageService _Garageservice;

        private readonly IMapper _mapper;

        public ManagerGarageController(IGarageService garageservice, IMapper mapper)
        {
            this._Garageservice = garageservice;
            this._mapper = mapper;
        }

        #region methods
        [Route("api/Garages/all")]
        public IHttpActionResult GetGarages()
        {
            ResponseDataDTO<IEnumerable<Garage>> response = new ResponseDataDTO<IEnumerable<Garage>>();
            try
            {
                response.Code = HttpCode.OK;
                response.Message = MessageResponse.SUCCESS;
                response.Data = _Garageservice.GetAll();
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

        [Route("api/Garages/page")]
        public IHttpActionResult GetGaragesPaging(int pageSize, int pageNumber)
        {
            ResponseDataDTO<PagedResults<Garage>> response = new ResponseDataDTO<PagedResults<Garage>>();
            try
            {
                response.Code = HttpCode.OK;
                response.Message = MessageResponse.SUCCESS;
                response.Data = _Garageservice.CreatePagedResults(pageNumber, pageSize);
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
        [Route("api/Garages/create")]
        public async Task<IHttpActionResult> CreateGarage()
        {
            ResponseDataDTO<int> response = new ResponseDataDTO<int>();
            try
            {
                var path = Path.GetTempPath();
                MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(path);
                // get data from formdata
                GarageViewModel GarageViewModel = new GarageViewModel
                {
                    GarageID = Guid.Parse(streamProvider.FormData["GarageID"]),
                    GarageName = Convert.ToString(streamProvider.FormData["GarageName"]),
                    Address = Convert.ToString(streamProvider.FormData["Address"]),
                    DateStart = Convert.ToDateTime(streamProvider.FormData["DateStart"]),
                    DateEnd = Convert.ToDateTime(streamProvider.FormData["DateEnd"]),
                };
                // mapping view model to entity
                var createdGarage = _mapper.Map<Garage>(GarageViewModel);
                // save new Garage
                _Garageservice.Create(createdGarage);
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
        [Route("api/Garages/update")]
        public async Task<IHttpActionResult> UpdateGarage()
        {
            ResponseDataDTO<int> response = new ResponseDataDTO<int>();
            try
            {
                var path = Path.GetTempPath();
                MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(path);
                GarageViewModel GarageViewModel = new GarageViewModel
                {
                    GarageID = Guid.Parse(streamProvider.FormData["GarageID"]),
                    GarageName = Convert.ToString(streamProvider.FormData["GarageName"]),
                    Address = Convert.ToString(streamProvider.FormData["Address"]),
                    DateStart = Convert.ToDateTime(streamProvider.FormData["DateStart"]),
                    DateEnd = Convert.ToDateTime(streamProvider.FormData["DateEnd"])
                };
                var existGarage = _Garageservice.Find(GarageViewModel.GarageID);
                
                // mapping view model to entity
                var updatedGarage = _mapper.Map<Garage>(GarageViewModel);
                // update quantity
                updatedGarage.GarageName = existGarage.GarageName;
                updatedGarage.Address = existGarage.Address;
                updatedGarage.DateStart = existGarage.DateStart;
                updatedGarage.DateEnd = existGarage.DateEnd;

                // update Garage
                _Garageservice.Update(updatedGarage, updatedGarage.GarageID);
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
        [Route("api/Garages/delete/{GarageId}")]
        public IHttpActionResult DeleteGarage(int GarageId)
        {
            ResponseDataDTO<int> response = new ResponseDataDTO<int>();
            try
            {
                var GarageDeleted = _Garageservice.Find(GarageId);
                if (GarageDeleted != null)
                {
                    _Garageservice.Delete(GarageDeleted);

                    // return response
                    response.Code = HttpCode.OK;
                    response.Message = MessageResponse.SUCCESS;
                    response.Data = 1;
                    return Ok(response);
                } else
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
                _Garageservice.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
