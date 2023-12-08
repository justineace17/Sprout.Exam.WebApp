using System;
using System.Collections.Generic;
using System.Text;
using Sprout.Exam.Common.Enums;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Birthdate { get; set; }
        public string Tin { get; set; }
        public int TypeId { get; set; }
        public string Type 
        { 
            get 
            {
                EmployeeType e = (EmployeeType)this.TypeId;
                return e.ToString();
            } 
        }
    }
}
