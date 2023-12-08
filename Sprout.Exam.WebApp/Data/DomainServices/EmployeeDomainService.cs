using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Models;

namespace Sprout.Exam.WebApp.Data.DomainServices
{
    public class EmployeeDomainService : IEmployeeDomainService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper ObjectMapper;

        public EmployeeDomainService(ApplicationDbContext context, IMapper ObjectMapper)
        {
            this.context = context;
            this.ObjectMapper = ObjectMapper;
        }

        public async Task<Employee> CreateAsync(Employee entity)
        {
            await context.Employees.AddAsync(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await context.Employees.AsQueryable().Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<Employee> GetAsync(Guid id)
        {
            var entity = await context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("Employee not found.");

            return entity;
        }

        public async Task<Employee> UpdateAsync(Employee input)
        {
            var entity = await context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == input.Id);

            if (entity == null)
                throw new Exception("Employee not found.");

            context.Employees.Update(input);
            context.SaveChanges();

            return input;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new ApplicationException("Employee not found.");

            context.Employees.Remove(entity);
            await context.SaveChangesAsync();
        }

       

       

        
    }
}
