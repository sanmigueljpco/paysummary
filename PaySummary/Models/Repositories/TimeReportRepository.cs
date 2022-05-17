using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaySummary.Models.Repositories
{
    public class TimeReportRepository : ITimeReportRepository
    {
        private readonly AppDbContext context;

        public TimeReportRepository(AppDbContext context)
        {
            this.context = context;
        }

        public virtual void CreateRecords(IEnumerable<TimeReport> records)
        {
            foreach (var item in records)
            {
                context.TimeReports.Add(item);
            }

            context.SaveChanges();
        }

        public virtual IEnumerable<TimeReport> GetAll()
        {
            return context.TimeReports.ToList();
        }
    }
}
