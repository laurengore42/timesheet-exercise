using Microsoft.AspNetCore.Mvc;
using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;
using Timesheets.Core.Services;

namespace Timesheets.Web.Ui.Controllers
{
    public class TimesheetController(TimesheetDbContext ctx, ITimesheetService timesheetService) : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Timesheet timesheet)
        {
            if (!ModelState.IsValid)
            {
                return View(timesheet);
            }

            var resp = timesheetService.AddTimesheet(timesheet);

            if (resp.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = resp.Message;
                return View(timesheet);
            }
        }

        public IActionResult Delete(int timesheetId)
        {
            var timesheet = ctx.Timesheets.Find(timesheetId)
                ?? throw new ArgumentOutOfRangeException(nameof(timesheetId));

            var resp = timesheetService.DeleteTimesheet(timesheet);

            if (resp.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = resp.Message;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
