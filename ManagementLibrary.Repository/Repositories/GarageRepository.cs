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
    public class GarageRepository : GenericRepository<Garage>, IGarageRepository
    {
        public GarageRepository(getwayDbContext dbContext) : base(dbContext)
        {
        }

        public PagedResults<Garage> CreatePagedResults(int pageNumber, int pageSize)
        {
            var skipAmount = pageSize * pageNumber;

            var list = _dbContext.Garages.OrderBy(t => t.GarageID).Skip(skipAmount).Take(pageSize);

            var totalNumberOfRecords = list.Count();

            var results = list.ToList();

            var mod = totalNumberOfRecords % pageSize;

            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            return new PagedResults<Garage>
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