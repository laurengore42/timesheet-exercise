using Timesheets.Core.Persistence.Models;
using Timesheets.Core.ViewModels;

namespace Timesheets.Core.Services
{
    public interface ITimesheetService
    {
        public bool AddTimesheet(Timesheet timesheet);
        public bool DeleteTimesheet(Timesheet timesheet);

        public IEnumerable<TimesheetViewModel> FetchAllTimesheets(bool stripCommas = false);
    }
}
