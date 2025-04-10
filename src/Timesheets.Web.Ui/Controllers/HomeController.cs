using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Timesheets.Core.Persistence;
using Timesheets.Web.Ui.ViewModels;

namespace Timesheets.Web.Ui.Controllers
{
    public class HomeController(TimesheetDbContext ctx) : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            var viewModel = (await ctx.Timesheets
                .Include(t => t.Person)
                .Include(t => t.Project)
                .ToListAsync())
                .Select(t => new TimesheetViewModel(t));

            return View(viewModel);
        }
    }
}
