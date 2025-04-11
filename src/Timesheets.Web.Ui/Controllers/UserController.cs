using Microsoft.AspNetCore.Mvc;
using Timesheets.Core.Persistence.Models;
using Timesheets.Core.Services;

namespace Timesheets.Web.Ui.Controllers
{
    public class UserController(IUserService userService) : Controller
    {
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public IActionResult Add(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

			if (userService.AddUser(user))
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
