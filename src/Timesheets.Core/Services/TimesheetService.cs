using Microsoft.EntityFrameworkCore;
using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;
using Timesheets.Core.ViewModels;

namespace Timesheets.Core.Services
{
    public class TimesheetService(TimesheetDbContext ctx) : ITimesheetService
    {
		public ServiceResponse AddTimesheet(Timesheet timesheet)
		{
			if (ctx.Users.FirstOrDefault(p => p.Id == timesheet.UserId) is null)
			{
                return new ServiceResponse()
                {
                    Success = false,
                    Message = "User not found"
                };
            }

			if (ctx.Projects.FirstOrDefault(p => p.Id == timesheet.ProjectId) is null)
            {
                return new ServiceResponse()
                {
                    Success = false,
                    Message = "Project not found"
                };
			}

			ctx.Timesheets.Add(timesheet);
			ctx.SaveChanges();

            return new ServiceResponse()
            {
                Success = true
            };
        }
        public ServiceResponse DeleteTimesheet(Timesheet timesheet)
        {
            ctx.Timesheets.Remove(timesheet);
            ctx.SaveChanges();

            return new ServiceResponse()
            {
                Success = true
            };
        }

        public IEnumerable<TimesheetViewModel> FetchAllTimesheets(bool stripCommas = false)
        {
            var hoursPerUserPerDay = Enumerable.ToList(Queryable.Select(Queryable.GroupBy(ctx.Timesheets, t => new { t.UserId, t.Date }), group => new { group.Key, HourSum = Enumerable.Sum(group, g => g.Hours) }));

            return ctx.Timesheets
                .Include(t => t.User)
                .Include(t => t.Project)
                .ToList()
                .Select(t => new TimesheetViewModel(t,
                    hoursPerUserPerDay.Find(hours =>
                        hours.Key.UserId == t.UserId &&
                        hours.Key.Date == t.Date)?
                    .HourSum ?? 0,
                    stripCommas));

        }
    }
}
