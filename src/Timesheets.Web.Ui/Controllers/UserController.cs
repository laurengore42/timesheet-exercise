using Microsoft.AspNetCore.Mvc;
using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;
using Timesheets.Core.Services;

namespace Timesheets.Web.Ui.Controllers
{
    public class UserController(TimesheetDbContext ctx, IUserService userService) : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var resp = userService.AddUser(user);

			if (resp.Success)
			{
				return RedirectToAction("Add", "Timesheet");
            }
            else
            {
                ViewBag.Error = resp.Message;
                return View(user);
            }
        }

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int userId)
        {
            var user = ctx.Users.Find(userId)
                ?? throw new ArgumentOutOfRangeException(nameof(userId));

            var resp = userService.DeleteUser(user);

            if (resp.Success)
            {
                return RedirectToAction("Add", "Timesheet");
            }
            else
            {
                ViewBag.Error = resp.Message;
                return View(user);
            }
        }
    }
}
