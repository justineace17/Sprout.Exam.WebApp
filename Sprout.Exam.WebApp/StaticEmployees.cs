using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Extensions;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;

namespace Sprout.Exam.WebApp
{
    public static class StaticEmployees
    {
        public static List<EmployeeDto> ResultList = new()
        {
            new EmployeeDto
            {
                Birthdate = "1993-03-25",
                FullName = "Jane Doe",
                Id = Guid.NewGuid(),
                Tin = "123215413",
                TypeId = 1
            },
            new EmployeeDto
            {
                Birthdate = "1993-05-28",
                FullName = "John Doe",
                 Id = Guid.NewGuid(),
                Tin = "957125412",
                TypeId = 2
            }
        };
    }
}
