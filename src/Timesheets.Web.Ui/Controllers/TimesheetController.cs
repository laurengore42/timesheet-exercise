using Microsoft.AspNetCore.Mvc;
using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;
using Timesheets.Web.Ui.ViewModels;

namespace Timesheets.Web.Ui.Controllers
{
	public class TimesheetController(TimesheetDbContext ctx) : Controller
	{
		public IActionResult Add()
		{
			var viewModel = new AddTimesheetViewModel
			{
				Persons = [.. ctx.Persons],
				Projects = [.. ctx.Projects]
			};
			return View("Add", viewModel);
		}

		[HttpPost]
		public IActionResult Add(string person, DateTime date, string project, string memo, decimal hours)
		{
			var newTimesheet = new Timesheet
			{
				Id = Guid.NewGuid(),
				PersonId = new Guid(person),
				Date = new DateOnly(date.Year, date.Month, date.Day),
				ProjectId = new Guid(project),
				Memo = memo,
				Hours = hours
			};

			ctx.Timesheets.Add(newTimesheet);
			ctx.SaveChanges();

			var viewModel = new AddTimesheetViewModel
			{
				Persons = [.. ctx.Persons],
				Projects = [.. ctx.Projects]
			};

			return View("Add", viewModel);
		}
	}
}
