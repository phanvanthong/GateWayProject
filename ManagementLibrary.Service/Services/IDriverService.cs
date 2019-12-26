using GetWay.Common.GenericService;
using GetWay.Common.Models;
using GetWay.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetWay.Service.Services
{
    public interface IDriverService : IGenericService<Driver>
    {
        PagedResults<Driver> CreatePagedResults(int pageNumber, int pageSize);

        IEnumerable<Driver> GetDriversByGarageID(Guid garageID);
    }

}