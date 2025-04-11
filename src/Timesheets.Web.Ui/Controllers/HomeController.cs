using Microsoft.AspNetCore.Mvc;
using Timesheets.Core.Services;

namespace Timesheets.Web.Ui.Controllers
{
    public class HomeController(ICsvService csvService, ITimesheetService timesheetService) : Controller
    {
        public IActionResult Index()
        {
            return View(timesheetService.FetchAllTimesheets());
        }

        public IActionResult CsvTimesheetExport()
        {
            csvService.CsvTimesheetExport();

            return Redirect("/export.csv");
        }
    }
}
