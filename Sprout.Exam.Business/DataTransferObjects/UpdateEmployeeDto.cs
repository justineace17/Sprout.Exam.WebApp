using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public class UpdateEmployeeDto: BaseSaveEmployeeDto
    {
        public Guid Id { get; set; }
    }
}
