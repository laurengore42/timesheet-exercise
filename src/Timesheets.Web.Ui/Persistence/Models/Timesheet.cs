namespace Timesheets.Web.Persistence.Models
{
	public class Timesheet
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public DateOnly Date { get; set; }
		public Guid ProjectId { get; set; }
		public string Memo { get; set; } = string.Empty;
		public decimal Hours { get; set; }
	}
}