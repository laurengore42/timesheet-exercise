using Timesheets.Core.Persistence.Models;

namespace Timesheets.Web.Ui.Services
{
    public interface ITimesheetService
    {
        public void AddTimesheet(Timesheet timesheet);
    }
}
