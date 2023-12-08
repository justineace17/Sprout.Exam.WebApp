using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Data.DomainServices;
using AutoMapper;
using Sprout.Exam.WebApp.Models;
using Abp.Domain.Entities;

namespace Sprout.Exam.WebApp.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper ObjectMapper;
        private readonly IEmployeeDomainService employeeDomainService;

        public EmployeesController(IEmployeeDomainService employeeDomainService, IMapper ObjectMapper)
        {
            this.employeeDomainService = employeeDomainService;
            this.ObjectMapper = ObjectMapper;
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var employees = await employeeDomainService.GetAllAsync();
            return Ok(MapEntityListDto(employees));
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var employees = await employeeDomainService.GetAsync(id);
            return Ok(MapEntityDto(employees));
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UpdateEmployeeDto input)
        {
            var employee = MapEntity(input);
            var employeeDto = await employeeDomainService.UpdateAsync(employee);
            return Ok(MapEntityDto(employeeDto));
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            var entity = MapEntity(input);
            await employeeDomainService.CreateAsync(entity);
            return Ok(MapEntityDto(entity));
        }

        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await employeeDomainService.DeleteAsync(id);
            return Ok(id);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(Guid id,decimal absentDays,decimal workedDays)
        {
            var employee = await employeeDomainService.GetAsync(id);

            if (employee == null) 
                return NotFound();

            var type = (EmployeeType)employee.TypeId;

            return type switch
            {
                EmployeeType.Regular =>
                    //create computation for regular.
                    Ok(Calculate(type, absentDays, workedDays)),
                EmployeeType.Contractual =>
                    //create computation for contractual.
                    Ok(Calculate(type, absentDays, workedDays)),
                _ => NotFound("Employee Type not found")
            };

        }

        private decimal Calculate(EmployeeType type, decimal absentDays, decimal workedDays)
        {
            switch (type)
            {
                case EmployeeType.Regular:
                    decimal monthlySalary = 20000;
                    decimal dailyDeduction = monthlySalary / 22;
                    decimal taxDeduction = monthlySalary * 0.12m;
                    decimal calculatedSalaryRegular = monthlySalary - (absentDays * dailyDeduction) - taxDeduction;
                    calculatedSalaryRegular = Math.Round(calculatedSalaryRegular, 2);

                    return calculatedSalaryRegular;

                case EmployeeType.Contractual:
                    decimal dailyRate = 500;
                    decimal calculatedSalaryContractual = workedDays * dailyRate;
                    calculatedSalaryContractual = Math.Round(calculatedSalaryContractual, 2);

                    return calculatedSalaryContractual;

                default:
                    return 0;
            }
        }

        private EmployeeDto MapEntityDto(Employee entity)
        {
            return ObjectMapper.Map<Employee, EmployeeDto>(entity);
        }

        private List<EmployeeDto> MapEntityListDto(List<Employee> entity)
        {
            return ObjectMapper.Map<List<Employee>, List<EmployeeDto>>(entity);
        }

        private Employee MapEntity(EmployeeDto entity)
        {
            return ObjectMapper.Map<EmployeeDto, Employee>(entity);
        }

        private Employee MapEntity(CreateEmployeeDto entity)
        {
            return ObjectMapper.Map<CreateEmployeeDto, Employee>(entity);
        }

        private Employee MapEntity(UpdateEmployeeDto entity)
        {
            return ObjectMapper.Map<UpdateEmployeeDto, Employee>(entity);
        }

    }
}
