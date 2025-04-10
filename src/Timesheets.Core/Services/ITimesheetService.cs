using Timesheets.Core.Persistence.Models;

namespace Timesheets.Core.Services
{
    public interface ITimesheetService
    {
        public bool AddTimesheet(Timesheet timesheet);
    }
}
