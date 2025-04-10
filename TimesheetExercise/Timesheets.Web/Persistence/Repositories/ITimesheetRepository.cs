using Timesheets.Web.Persistence.Models;

namespace Timesheets.Web.Persistence.Repositories
{
	public interface ITimesheetRepository
	{
		public IEnumerable<TimesheetDto> GetTimesheets();
	}
}
