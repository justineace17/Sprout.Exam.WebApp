using AutoMapper;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Models;

namespace Sprout.Exam.WebApp.Data
{
    public class ApplicationMapProfile : Profile
    {
        public ApplicationMapProfile()
        {
            CreateMap<EmployeeDto, Employee>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();

            CreateMap<CreateEmployeeDto, Employee>().ReverseMap();
            CreateMap<Employee, CreateEmployeeDto>().ReverseMap();

            CreateMap<UpdateEmployeeDto, Employee>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeDto>().ReverseMap();

        }
    }
}
