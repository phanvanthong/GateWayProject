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
    public class StudentService : GenericService<Student>, IStudentService
    {
        private readonly IStudentRepository _repository;
        public StudentService(IStudentRepository repository) : base(repository)
        {
            this._repository = repository;
        }

        public PagedResults<Student> CreatePagedResults(int pageNumber, int pageSize)
        {
            return this._repository.CreatePagedResults(pageNumber, pageSize);
        }

        public IEnumerable<Student> GetStudentsByPlanID(Guid planID)
        {
            return this._repository.GetStudentsByPlanID(planID);
        }
    }
}