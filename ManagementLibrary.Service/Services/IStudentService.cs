using GetWay.Common.GenericService;
using GetWay.Common.Models;
using GetWay.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetWay.Service.Services
{
    public interface IStudentService : IGenericService<Student>
    {
        PagedResults<Student> CreatePagedResults(int pageNumber, int pageSize);

        IEnumerable<Student> GetStudentsByPlanID(Guid planID);
    }

}