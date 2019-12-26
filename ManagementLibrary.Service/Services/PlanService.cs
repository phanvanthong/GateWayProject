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
    public class PlanService : GenericService<Plan>, IPlanService
    {
        private readonly IPlanRepository _repository;
        public PlanService(IPlanRepository repository) : base(repository)
        {
            this._repository = repository;
        }

        public PagedResults<Plan> CreatePagedResults(int pageNumber, int pageSize)
        {
            return this._repository.CreatePagedResults(pageNumber, pageSize);
        }

        public Plan GetPlanByID(Guid planID)
        {
            return this._repository.GetPlanByID(planID);
        }
    }
}