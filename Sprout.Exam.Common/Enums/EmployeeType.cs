using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sprout.Exam.Common.Enums
{
    public enum EmployeeType
    {
        [Description("Regular")]
        Regular = 1,
        [Description("Contractual")]
        Contractual = 2
    }
}
