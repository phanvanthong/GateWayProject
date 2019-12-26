using GetWay.Common.GenericRepository;
using GetWay.Common.Models;
using GetWay.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetWay.Repository.Repositories
{
    public interface IGarageRepository : IGenericRepository<Garage>
    {
        PagedResults<Garage> CreatePagedResults(int pageNumber, int pageSize);
    }
}