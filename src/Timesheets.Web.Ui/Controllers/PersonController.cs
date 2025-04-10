using Microsoft.AspNetCore.Mvc;
using Timesheets.Core.Persistence.Models;
using Timesheets.Web.Ui.Services;

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

			return View(person);
        }
    }
}
