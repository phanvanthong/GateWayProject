using AutoMapper;
using GetWay.API.Models;
using GetWay.Data.Models;
using GetWay.Common.Constants;
using GetWay.Data.DbContext;
using GetWay.Data.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ManagementLibrary.API.Controllers.Authentication
{
    public class ManagerUserController : ApiController
    {
        private getwayDbContext _gateWayDbContext = new getwayDbContext();

        public IMapper _mapper;

        private UserManager<IdentityUser> _userManager;


        [HttpGet]
        [Route("api/Users/Login")]
        public IHttpActionResult Login()
        {
            
            ResponseDataDTO<int> response = new ResponseDataDTO<int>();
            try
            {
                var path = Path.GetTempPath();
                MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(path);

                // get data from formdata
                UserViewModel userViewModel = new UserViewModel
                {
                    UserName = Convert.ToString(streamProvider.FormData["UserName"]),
                    Password = Convert.ToString(streamProvider.FormData["Password"])
                };
                // mapping view model to entity
                var createdCar = _mapper.Map<AspNetUser>(userViewModel);
                //var _user = _gateWayDbContext.AspNetUsers.SingleOrDefault(n => n.UserName == userViewModel.UserName &&  _userManager.HasPasswordAsync(userViewModel,n.PasswordHash));
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
    }
}
