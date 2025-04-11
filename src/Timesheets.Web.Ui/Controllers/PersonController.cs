﻿using Microsoft.AspNetCore.Mvc;
using Timesheets.Core.Persistence.Models;
using Timesheets.Core.Services;

namespace Timesheets.Web.Ui.Controllers
{
    public class PersonController(IPersonService personService) : Controller
    {
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public IActionResult Add(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

			if (personService.AddPerson(person))
			{
				return RedirectToAction("Add", "Timesheet");
            }
            else
            {
                throw new InvalidOperationException("Failed to save user");
            }
        }
    }
}
