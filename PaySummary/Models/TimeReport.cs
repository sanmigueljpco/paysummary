using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaySummary.Models
{
    public class TimeReport
    {
        [Key]
        [Ignore]
        public int TimeReportId { get; set; }

        [Name("employee id")]
        public int EmployeeId { get; set; }

        [Name("date")]
        public string Date { get; set; }

        [Name("hours worked")]
        public decimal HoursWorked { get; set; }

        [Name("job group")]
        public string JobGroup { get; set; }
    }
}
