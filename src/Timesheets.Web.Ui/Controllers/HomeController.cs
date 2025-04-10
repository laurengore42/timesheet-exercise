using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Timesheets.Core.Persistence;
using Timesheets.Core.Services;
using Timesheets.Core.ViewModels;

namespace Timesheets.Web.Ui.Controllers
{
    public class HomeController(TimesheetDbContext ctx, ICsvService csvService) : Controller
    {
        public IActionResult Index()
        {
            if (ctx.Timesheets is null)
            {
                throw new InvalidOperationException("Could not access database tables");
            }

            var hoursPerPersonPerDay = ctx.Timesheets
                .GroupBy(t => new { t.PersonId, t.Date })
                .Select(group => new { group.Key, HourSum = group.Sum(g => g.Hours) })
                .ToList();

            var viewModel = ctx.Timesheets
                .Include(t => t.Person)
                .Include(t => t.Project)
                .ToList()
                .Select(t => new TimesheetViewModel(t,
                    hoursPerPersonPerDay.Find(
                        hours => hours.Key.PersonId == t.PersonId &&
                                 hours.Key.Date == t.Date)?
                        .HourSum ?? 0));

            return View(viewModel);
        }

        public IActionResult Export()
        {
            csvService.Export();

            return RedirectToAction("Index");
        }
    }
}
