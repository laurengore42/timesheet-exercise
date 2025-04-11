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
			if (ctx.Timesheets is null || ctx.Users is null || ctx.Projects is null)
			{
				throw new InvalidOperationException("Could not access database tables");
			}

			if (ctx.Users.FirstOrDefault(p => p.Id == timesheet.UserId) is null)
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

        public IEnumerable<TimesheetViewModel> FetchAllTimesheets(bool stripCommas = false)
        {
            if (ctx.Timesheets is null)
            {
                throw new InvalidOperationException("Could not access database tables");
            }

            var hoursPerUserPerDay = Enumerable.ToList(Queryable.Select(Queryable.GroupBy(ctx.Timesheets, t => new { t.UserId, t.Date }), group => new { group.Key, HourSum = Enumerable.Sum(group, g => g.Hours) }));

            return ctx.Timesheets
                .Include(t => t.User)
                .Include(t => t.Project)
                .ToList()
                .Select(t => new TimesheetViewModel(t,
                    hoursPerUserPerDay.Find(
                        hours => hours.Key.UserId == t.UserId &&
                                 hours.Key.Date == t.Date)?
                        .HourSum ?? 0,
                    stripCommas));

        }
    }
}
