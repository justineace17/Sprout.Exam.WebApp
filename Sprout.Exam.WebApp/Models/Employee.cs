using System;
using Abp.Domain.Entities.Auditing;

namespace Sprout.Exam.WebApp.Models
{
    public class Employee : FullAuditedEntity<Guid>
    {
        public string FullName { get; set; }
        public string Birthdate { get; set; }
        public string Tin { get; set; }
        public int TypeId { get; set; }
    }
}
