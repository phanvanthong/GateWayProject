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
    public class ManagerCarController : ApiController
    {
        private readonly ICarService _Carservice;

        private readonly IMapper _mapper;

        public ManagerCarController(ICarService carservice, IMapper mapper)
        {
            this._Carservice = carservice;
            this._mapper = mapper;
        }

        #region methods
        [Route("api/Cars/all")]
        public IHttpActionResult GetCars()
        {
            ResponseDataDTO<IEnumerable<Car>> response = new ResponseDataDTO<IEnumerable<Car>>();
            try
            {
                response.Code = HttpCode.OK;
                response.Message = MessageResponse.SUCCESS;
                response.Data = _Carservice.GetAll();
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

        [Route("api/Cars/page")]
        public IHttpActionResult GetCarsPaging(int pageSize, int pageNumber)
        {
            ResponseDataDTO<PagedResults<Car>> response = new ResponseDataDTO<PagedResults<Car>>();
            try
            {
                response.Code = HttpCode.OK;
                response.Message = MessageResponse.SUCCESS;
                response.Data = _Carservice.CreatePagedResults(pageNumber, pageSize);
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
        [Route("api/Cars/create")]
        public async Task<IHttpActionResult> CreateCar()
        {
            ResponseDataDTO<string> response = new ResponseDataDTO<string>();
            try
            {
                var path = Path.GetTempPath();

                if (!Request.Content.IsMimeMultipartContent("form-data"))
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.UnsupportedMediaType));
                }
                
                MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(path);

                await Request.Content.ReadAsMultipartAsync(streamProvider);
                // save file
                string fileName = "";
                foreach (MultipartFileData fileData in streamProvider.FileData)
                {
                    fileName = FileExtension.SaveFileOnDisk(fileData);
                }
                // get data from formdata
                CarCreateViewModel CarCreateViewModel = new CarCreateViewModel
                {
                    CarNumber = Convert.ToString(streamProvider.FormData["CarNumber"])
                };
                // mapping view model to entity
                var createdCar = _mapper.Map<Car>(CarCreateViewModel);
                // save new Car
                _Carservice.Create(createdCar);
                // return response
                response.Code = HttpCode.OK;
                response.Message = MessageResponse.SUCCESS;
                response.Data = fileName;
                return Ok(response);
            } catch (Exception ex)
            {
                response.Code = HttpCode.INTERNAL_SERVER_ERROR;
                response.Message = MessageResponse.FAIL;
                response.Data = null;
                Console.WriteLine(ex.ToString());

                return Ok(response);
            }

        }


        [HttpPut]
        [Route("api/Cars/update")]
        public async Task<IHttpActionResult> UpdateCar()
        {
            ResponseDataDTO<int> response = new ResponseDataDTO<int>();
            try
            {
                var path = Path.GetTempPath();

                if (!Request.Content.IsMimeMultipartContent("form-data"))
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.UnsupportedMediaType));
                }

                MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(path);

                await Request.Content.ReadAsMultipartAsync(streamProvider);
                // save file
                string fileName = "";
                if (streamProvider.FileData.Count > 0)
                {
                    foreach (MultipartFileData fileData in streamProvider.FileData)
                    {
                        fileName = FileExtension.SaveFileOnDisk(fileData);
                    }
                }
                
                // get data from formdata
                CarUpdateViewModel CarUpdateViewModel = new CarUpdateViewModel
                {
                    CarNumber = Convert.ToString(streamProvider.FormData["CarNumber"])
                };
                var existCar = _Carservice.Find(CarUpdateViewModel.CarNumber);
                if (fileName != "")
                {
                    CarUpdateViewModel.CarNumber = fileName;
                } else
                {
                    
                    CarUpdateViewModel.CarNumber = existCar.CarNumber;
                }
                // mapping view model to entity
                var updatedCar = _mapper.Map<Car>(CarUpdateViewModel);
                // update quantity
                updatedCar.CarNumber = existCar.CarNumber;

                // update Car
                _Carservice.Update(updatedCar, updatedCar.CarNumber);
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
        [Route("api/Cars/delete/{CarId}")]
        public IHttpActionResult DeleteCar(int CarId)
        {
            ResponseDataDTO<int> response = new ResponseDataDTO<int>();
            try
            {
                var CarDeleted = _Carservice.Find(CarId);
                if (CarDeleted != null)
                {
                    _Carservice.Delete(CarDeleted);

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
                _Carservice.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
