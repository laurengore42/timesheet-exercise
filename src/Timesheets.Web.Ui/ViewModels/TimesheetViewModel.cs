using Timesheets.Web.Persistence.Models;

namespace Timesheets.Web.ViewModels
{
	public class TimesheetViewModel(Timesheet t)
	{
		public string PersonName { get; } = t.Person?.Name ?? throw new ArgumentNullException(nameof(t.Person));
		public string Date { get; } = t.Date.ToString("dd/MM/yyyy");
		public string ProjectName { get; } = t.Project?.Name ?? throw new ArgumentNullException(nameof(t.Project));
		public string Memo { get; } = t.Memo;
		public decimal Hours { get; } = t.Hours;
	}
}
