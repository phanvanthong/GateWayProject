using GetWay.Common.GenericRepository;
using GetWay.Common.Models;
using GetWay.Data.DbContext;
using GetWay.Data.Models;
using GetWay.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetWay.Repository.Repositories
{
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        public DriverRepository(getwayDbContext dbContext) : base(dbContext)
        {
        }

        public PagedResults<Driver> CreatePagedResults(int pageNumber, int pageSize)
        {
            var skipAmount = pageSize * pageNumber;

            var list = _dbContext.Drivers.OrderBy(t => t.DriverID).Skip(skipAmount).Take(pageSize);

            var totalNumberOfRecords = list.Count();

            var results = list.ToList();

            var mod = totalNumberOfRecords % pageSize;

            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            return new PagedResults<Driver>
            {
                Results = results,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalNumberOfPages = totalPageCount,
                TotalNumberOfRecords = totalNumberOfRecords
            };
        }

        public IEnumerable<Driver> GetDriversByGarageID(Guid garageID)
        {
            return _dbContext.Drivers.Where(t => t.GarageID==garageID);
        }
    }
}