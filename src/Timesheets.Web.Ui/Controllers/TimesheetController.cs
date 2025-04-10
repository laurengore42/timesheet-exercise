using Microsoft.AspNetCore.Mvc;
using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;

namespace Timesheets.Web.Ui.Controllers
{
	public class TimesheetController(TimesheetDbContext ctx) : Controller
	{
		public IActionResult Add()
		{
			return View("Add", "Add your timesheet entry below");
		}

		[HttpPost]
		public IActionResult Add(decimal hours, string memo, DateTime date)
		{
			var newTimesheet = new Timesheet
			{
				Id = Guid.NewGuid(),
				PersonId = new Guid("d9239d95-b0a2-4577-baf6-a6abdaa8a304"),
				Date = new DateOnly(date.Year, date.Month, date.Day),
				ProjectId = new Guid("230bda5e-3004-4690-bd4d-1127e4f3e994"),
				Memo = memo,
				Hours = hours
			};

			ctx.Timesheets.Add(newTimesheet);
			ctx.SaveChanges();

			return View("Add", "Success");
		}
	}
}
