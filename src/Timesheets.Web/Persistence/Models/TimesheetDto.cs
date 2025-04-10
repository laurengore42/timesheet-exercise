namespace Timesheets.Web.Persistence.Models
{
	public class TimesheetDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public DateOnly Date { get; set; }
		public string Project { get; set; } = string.Empty;
		public string Memo { get; set; } = string.Empty;
		public decimal Hours { get; set; }
	}
}