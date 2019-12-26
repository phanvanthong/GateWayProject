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
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        public CarRepository(getwayDbContext dbContext) : base(dbContext)
        {
        }

        public PagedResults<Car> CreatePagedResults(int pageNumber, int pageSize)
        {
            var skipAmount = pageSize * pageNumber;

            var list = _dbContext.Cars.OrderBy(t => t.CarID).Skip(skipAmount).Take(pageSize);

            var totalNumberOfRecords = list.Count();

            var results = list.ToList();

            var mod = totalNumberOfRecords % pageSize;

            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            return new PagedResults<Car>
            {
                Results = results,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalNumberOfPages = totalPageCount,
                TotalNumberOfRecords = totalNumberOfRecords
            };
        }
    }
}