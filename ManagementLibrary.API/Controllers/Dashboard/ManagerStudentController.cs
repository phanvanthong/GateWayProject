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
    public class ManagerStudentController : ApiController
    {
        private readonly IStudentService _Studentservice;

        private readonly IMapper _mapper;

        public ManagerStudentController(IStudentService Studentservice, IMapper mapper)
        {
            this._Studentservice = Studentservice;
            this._mapper = mapper;
        }

        #region methods
        [Route("api/Students/all")]
        public IHttpActionResult GetStudents()
        {
            ResponseDataDTO<IEnumerable<Student>> response = new ResponseDataDTO<IEnumerable<Student>>();
            try
            {
                response.Code = HttpCode.OK;
                response.Message = MessageResponse.SUCCESS;
                response.Data = _Studentservice.GetAll();
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

        [Route("api/Students/page")]
        public IHttpActionResult GetStudentsPaging(int pageSize, int pageNumber)
        {
            ResponseDataDTO<PagedResults<Student>> response = new ResponseDataDTO<PagedResults<Student>>();
            try
            {
                response.Code = HttpCode.OK;
                response.Message = MessageResponse.SUCCESS;
                response.Data = _Studentservice.CreatePagedResults(pageNumber, pageSize);
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

        [Route("api/Students/getByPlanID")]
        public async Task<IHttpActionResult> GetStudentsByPlanID(Guid PlandID)
        {
            ResponseDataDTO<IEnumerable<Student>> response = new ResponseDataDTO<IEnumerable<Student>>();
            try
            {
                response.Code = HttpCode.OK;
                response.Message = MessageResponse.SUCCESS;
                response.Data = _Studentservice.GetStudentsByPlanID(PlandID);
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

        #endregion

        #region dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _Studentservice.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
