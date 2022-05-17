using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaySummary.Dtos;
using PaySummary.Models;
using PaySummary.Models.Repositories;

namespace PaySummary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITimeReportRepository timeReportRepository;

        public HomeController(ITimeReportRepository timeReportRepository)
        {
            this.timeReportRepository = timeReportRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UploadCsv(IFormFile selectedFile)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", selectedFile.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError("", "Not valid. Duplicate report.");
                return View("Index");
            }

            using (var fileStream = System.IO.File.Create(path))
            {
                selectedFile.CopyTo(fileStream);
            }

            using (var streamReader = new StreamReader(path))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InstalledUICulture))
                {
                    var records = csvReader.GetRecords<TimeReport>();

                    timeReportRepository.CreateRecords(records);
                }
            }

            return View("Index");
        }

        public JsonResult GetPayrollReport()
        {
            var timeReports = timeReportRepository.GetAll();

            var reportPeriodGroupings = timeReports.Select(x => new
            {
                x.EmployeeId,
                Date = IdentifyPeriod(x.Date),
                Amount = GetAmount(x.HoursWorked, x.JobGroup)
            });

            return Json(new ResponseDto
            {
                PayrollReport = new PayrollReportDto
                {
                    EmployeeReports = reportPeriodGroupings
                        .GroupBy(x => new { x.EmployeeId, x.Date })
                        .Select(x => new EmployeeReportDto
                        {
                            EmployeeId = x.Key.EmployeeId,
                            AmountPaid = x.Sum(s => s.Amount),
                            PayPeriod = GetPeriod(x.Key.Date)
                        }).ToList()
                }
            });
        }

        public DateTime IdentifyPeriod(string date)
        {
            var dateParts = date.Split('/');

            var day = Convert.ToInt16(dateParts[0]);
            var month = Convert.ToInt16(dateParts[1]);
            var year = Convert.ToInt16(dateParts[2]);

            if (day < 16)
            {
                return new DateTime(year, month, 1);
            }
            else
            {
                return new DateTime(year, month, 16);
            }
        }

        public decimal GetAmount(decimal hours, string group)
        {
            if (group.ToLower() == "a")
            {
                return hours * 20;
            }
            else if (group.ToLower() == "b")
            {
                return hours * 30;
            }
            else
            {
                return 0;
            }
        }

        public PayPeriodDto GetPeriod(DateTime date)
        {
            if (date.Day < 16)
            {
                return new PayPeriodDto
                {
                    StartDate = new DateTime(date.Year, date.Month, 1).ToString("d"),
                    EndDate = new DateTime(date.Year, date.Month, 15).ToString("d"),
                };
            }
            else
            {
                var endDay = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

                return new PayPeriodDto
                {
                    StartDate = new DateTime(date.Year, date.Month, 16).ToString("d"),
                    EndDate = new DateTime(date.Year, date.Month, endDay.Day).ToString("d"),
                };
            }
        }
    }
}