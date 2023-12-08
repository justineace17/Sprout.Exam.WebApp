using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Data.DomainServices
{
    public interface IEmployeeDomainService
    {
        Task<Employee> CreateAsync(Employee entity);
        Task<List<Employee>> GetAllAsync();
        Task<Employee> GetAsync(Guid id);
        Task<Employee> UpdateAsync(Employee input);
        Task DeleteAsync(Guid id);
    }
}
