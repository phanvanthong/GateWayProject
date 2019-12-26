using GetWay.Common.GenericRepository;
using GetWay.Common.Models;
using GetWay.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetWay.Repository.Repositories
{
    public interface IPlanRepository : IGenericRepository<Plan>
    {
        PagedResults<Plan> CreatePagedResults(int pageNumber, int pageSize);

        Plan GetPlanByID(Guid planID);
    }
}