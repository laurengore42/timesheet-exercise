using Microsoft.AspNetCore.Mvc;
using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;
using Timesheets.Web.Ui.Services;

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

            // TO DO handle a No here
            // TO DO we need 'add person' and 'add project' forms
            // TO DO display validation for modelstate invalid?
            if (timesheetService.AddTimesheet(timesheet))
			{
				return RedirectToAction("Index", "Home");
			}

			return View(timesheet);
        }
    }
}
