using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.Web.Persistence.Models
{
	public class Timesheet
	{
		public Guid Id { get; set; }
		public Guid PersonId { get; set; }
		public DateOnly Date { get; set; }
		public Guid ProjectId { get; set; }
		public string Memo { get; set; } = string.Empty;
		public decimal Hours { get; set; }

		[ForeignKey("PersonId")]
		public Person? Person { get; set; }
		[ForeignKey("ProjectId")]
		public Project? Project { get; set; }
	}
}