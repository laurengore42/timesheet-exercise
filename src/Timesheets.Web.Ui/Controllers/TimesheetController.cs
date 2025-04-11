using Microsoft.AspNetCore.Mvc;
using Timesheets.Core.Persistence.Models;
using Timesheets.Core.Services;

namespace Timesheets.Web.Ui.Controllers
{
    public class TimesheetController(ITimesheetService timesheetService) : Controller
    {
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public IActionResult Add(Timesheet timesheet)
        {
            if (!ModelState.IsValid)
            {
                return View(timesheet);
            }

            if (timesheetService.AddTimesheet(timesheet))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                throw new InvalidOperationException("Failed to save timesheet");
            }
        }
    }
}
