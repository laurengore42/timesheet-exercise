using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;

namespace Timesheets.Web.Ui.Services
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
    }
}
