using Microsoft.EntityFrameworkCore;
using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;
using Timesheets.Core.ViewModels;

namespace Timesheets.Core.Services
{
    public class TimesheetService(TimesheetDbContext ctx) : ITimesheetService
    {
		public bool AddTimesheet(Timesheet timesheet)
		{
			if (ctx.Timesheets is null || ctx.Persons is null || ctx.Projects is null)
			{
				throw new InvalidOperationException("Could not access database tables");
			}

			if (ctx.Persons.FirstOrDefault(p => p.Id == timesheet.PersonId) is null)
			{
				return false;
			}

			if (ctx.Projects.FirstOrDefault(p => p.Id == timesheet.ProjectId) is null)
			{
				return false;
			}

			ctx.Timesheets.Add(timesheet);
			ctx.SaveChanges();

			return true;
		}

        public IEnumerable<TimesheetViewModel> FetchAllTimesheets()
        {
            if (ctx.Timesheets is null)
            {
                throw new InvalidOperationException("Could not access database tables");
            }

            var hoursPerPersonPerDay = ctx.Timesheets
                .GroupBy(t => new { t.PersonId, t.Date })
                .Select(group => new { group.Key, HourSum = group.Sum(g => g.Hours) })
                .ToList();

            return ctx.Timesheets
                .Include(t => t.Person)
                .Include(t => t.Project)
                .ToList()
                .Select(t => new TimesheetViewModel(t,
                    hoursPerPersonPerDay.Find(
                        hours => hours.Key.PersonId == t.PersonId &&
                                 hours.Key.Date == t.Date)?
                        .HourSum ?? 0));

        }
    }
}
