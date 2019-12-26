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
    public class GarageService : GenericService<Garage>, IGarageService
    {
        private readonly IGarageRepository _repository;
        public GarageService(IGarageRepository repository) : base(repository)
        {
            this._repository = repository;
        }

        public PagedResults<Garage> CreatePagedResults(int pageNumber, int pageSize)
        {
            return this._repository.CreatePagedResults(pageNumber, pageSize);
        }
    }
}