using Timesheets.Web.Persistence.Models;

namespace Timesheets.Web.ViewModels
{
	public class TimesheetViewModel(Timesheet t)
	{
		public string Name { get; } = t.Name;
		public string Date { get; } = t.Date.ToString("dd/MM/yyyy");
		public string ProjectName { get; } = t.Project?.Name ?? "pardon?";
		public string Memo { get; } = t.Memo;
		public decimal Hours { get; } = t.Hours;
	}
}
