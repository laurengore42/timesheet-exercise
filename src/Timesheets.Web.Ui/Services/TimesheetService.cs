using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;

namespace Timesheets.Web.Ui.Services
{
    public class TimesheetService(TimesheetDbContext ctx) : ITimesheetService
    {
        public void AddTimesheet(Timesheet timesheet)
		{
			ctx.Timesheets.Add(timesheet);
			ctx.SaveChanges();
		}
    }
}
