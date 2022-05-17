using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaySummary.Models.Repositories
{
    public interface ITimeReportRepository
    {
        void CreateRecords(IEnumerable<TimeReport> records);
        IEnumerable<TimeReport> GetAll();
    }
}
