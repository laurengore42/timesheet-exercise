using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Timesheets.Core.Persistence.Models
{
    [PrimaryKey(nameof(Id))]
    public class Timesheet
    {
        public int Id { get; set; }

        public required int UserId { get; set; }

        public required DateOnly Date { get; set; }

        public required int ProjectId { get; set; }

        public required decimal Hours { get; set; }

        public required string Memo { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
		[ForeignKey(nameof(ProjectId))]
        public Project? Project { get; set; }
    }
}
