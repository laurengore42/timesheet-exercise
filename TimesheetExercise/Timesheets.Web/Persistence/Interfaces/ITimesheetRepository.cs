using Timesheets.Web.Persistence.Models;

namespace Timesheets.Web.Persistence.Interfaces
{
	public interface ITimesheetRepository
	{
		public IEnumerable<TimesheetDto> GetTimesheets();
	}
}
