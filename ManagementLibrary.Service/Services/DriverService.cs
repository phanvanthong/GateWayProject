using GetWay.Common.GenericRepository;
using GetWay.Common.GenericService;
using GetWay.Common.Models;
using GetWay.Data.Models;
using GetWay.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetWay.Service.Services
{
    public class DriverService : GenericService<Driver>, IDriverService
    {
        private readonly IDriverRepository _repository;
        public DriverService(IDriverRepository repository) : base(repository)
        {
            this._repository = repository;
        }

        public PagedResults<Driver> CreatePagedResults(int pageNumber, int pageSize)
        {
            return this._repository.CreatePagedResults(pageNumber, pageSize);
        }

        public IEnumerable<Driver> GetDriversByGarageID(Guid garageID)
        {
            return this._repository.GetDriversByGarageID(garageID);
        }
    }
}