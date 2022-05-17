using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaySummary.Dtos
{
    public class EmployeeReportDto
    {
        public int EmployeeId { get; set; }
        public PayPeriodDto PayPeriod { get; set; }
        public decimal AmountPaid { get; set; }
    }
}
