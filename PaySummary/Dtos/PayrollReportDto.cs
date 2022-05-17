using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaySummary.Dtos
{
    public class PayrollReportDto
    {
        public IEnumerable<EmployeeReportDto> EmployeeReports { get; set; }
    }
}
