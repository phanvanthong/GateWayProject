using GetWay.Common.GenericRepository;
using GetWay.Common.Models;
using GetWay.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetWay.Repository.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        PagedResults<Student> CreatePagedResults(int pageNumber, int pageSize);

        IEnumerable<Student> GetStudentsByPlanID(Guid planID);
    }
}