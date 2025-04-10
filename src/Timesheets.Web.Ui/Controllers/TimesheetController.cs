﻿using Microsoft.AspNetCore.Mvc;
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

            // TO DO handle a No here
            // TO DO display validation for modelstate invalid?
            // TO DO CSV export
            if (timesheetService.AddTimesheet(timesheet))
			{
				return RedirectToAction("Index", "Home");
			}

			return View(timesheet);
        }
    }
}
