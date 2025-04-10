using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.Core.Persistence.Models
{
	[PrimaryKey(nameof(Id))]
	public class Timesheet
	{
		public int Id { get; set; }

		public required int PersonId { get; set; }

		public required DateOnly Date { get; set; }

		public required int ProjectId { get; set; }

		public required decimal Hours { get; set; }

		public required string Memo { get; set; }

		[ForeignKey("PersonId")]
		public Person? Person { get; set; }
		[ForeignKey("ProjectId")]
		public Project? Project { get; set; }
	}
}