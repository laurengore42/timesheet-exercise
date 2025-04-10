using Microsoft.AspNetCore.Mvc;
using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;

namespace Timesheets.Web.Ui.Controllers
{
    public class TimesheetController(TimesheetDbContext ctx) : Controller
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

            ctx.Timesheets.Add(timesheet);
            ctx.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
