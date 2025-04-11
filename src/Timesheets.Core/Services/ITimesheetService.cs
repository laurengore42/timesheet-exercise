using Timesheets.Core.Persistence.Models;
using Timesheets.Core.ViewModels;

namespace Timesheets.Core.Services
{
    public interface ITimesheetService
    {
        public ServiceResponse AddTimesheet(Timesheet timesheet);
        public ServiceResponse DeleteTimesheet(Timesheet timesheet);

        public IEnumerable<TimesheetViewModel> FetchAllTimesheets(bool stripCommas = false);
    }
}
