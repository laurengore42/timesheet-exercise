using Microsoft.AspNetCore.Mvc;
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

			// TO DO handle a No here
			// TO DO display validation for modelstate invalid?
			if (personService.AddPerson(person))
			{
				return RedirectToAction("Add", "Timesheet");
			}

			return View(person);
        }
    }
}
