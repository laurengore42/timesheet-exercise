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

			if (userService.AddUser(user))
			{
				return RedirectToAction("Add", "Timesheet");
            }
            else
            {
                ViewBag.Error = "Failed to save user";
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

            if (ctx.Timesheets.Any(t => t.UserId == userId))
            {
                ViewBag.Error = "Cannot delete a user with existing timesheet rows";
                return View();
            }

            if (userService.DeleteUser(user))
            {
                return RedirectToAction("Add", "Timesheet");
            }
            else
            {
                throw new InvalidOperationException("Failed to delete user");
            }
        }
    }
}
