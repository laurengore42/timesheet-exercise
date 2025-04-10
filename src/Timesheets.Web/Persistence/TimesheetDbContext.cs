using Microsoft.EntityFrameworkCore;
using Timesheets.Core.Persistence.Models;

namespace Timesheets.Core.Persistence
{
	public class TimesheetDbContext(DbContextOptions<TimesheetDbContext> options) : DbContext(options)
	{
		public DbSet<Timesheet> Timesheets { get; set; }
		public DbSet<Project> Projects { get; set; }
	}
}
