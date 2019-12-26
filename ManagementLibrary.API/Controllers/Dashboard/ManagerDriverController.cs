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
    public class ManagerDriverController : ApiController
    {
        private readonly IDriverService _driverservice;

        private readonly IMapper _mapper;

        public ManagerDriverController(IDriverService driverservice, IMapper mapper)
        {
            this._driverservice = driverservice;
            this._mapper = mapper;
        }

        #region methods
        [Route("api/Drivers/{garageID}")]
        public IHttpActionResult GetDrivers(Guid garageID)
        {
            ResponseDataDTO<IEnumerable<Driver>> response = new ResponseDataDTO<IEnumerable<Driver>>();
            try
            {
                response.Code = HttpCode.OK;
                response.Message = MessageResponse.SUCCESS;
                response.Data = _driverservice.GetDriversByGarageID(garageID);
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
                _driverservice.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
