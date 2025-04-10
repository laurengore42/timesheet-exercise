using Timesheets.Core.Persistence.Models;

namespace Timesheets.Web.Ui.ViewModels
{
	public class AddTimesheetViewModel
	{
		public List<Person> Persons { get; set; } = [];
		public List<Project> Projects { get; set; } = [];
	}
}
