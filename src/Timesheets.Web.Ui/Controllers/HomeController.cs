using Microsoft.AspNetCore.Mvc;
using Timesheets.Core.Persistence;
using Timesheets.Core.Services;

namespace Timesheets.Web.Ui.Controllers
{
    public class HomeController(TimesheetDbContext ctx, ICsvService csvService, ITimesheetService timesheetService) : Controller
    {
        public IActionResult Index()
        {
            if (ctx.Timesheets is null)
            {
                throw new InvalidOperationException("Could not access database tables");
            }

            return View(timesheetService.FetchAllTimesheets());
        }

        public IActionResult CsvTimesheetExport()
        {
            return csvService.CsvTimesheetExport();
        }
    }
}
