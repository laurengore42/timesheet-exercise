using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Timesheets.Web.Persistence;
using Timesheets.Web.ViewModels;

namespace Timesheets.Web.Controllers
{
	public class HomeController(TimesheetDbContext ctx) : Controller
	{
		public async Task<IActionResult> IndexAsync()
		{
			var viewModel = (await ctx.Timesheets
				.Include(t => t.Project)
				.ToListAsync())
				.Select(t => new TimesheetViewModel(t));

			return View(viewModel);
		}
	}
}
