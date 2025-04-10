using Microsoft.EntityFrameworkCore;
using Timesheets.Web.Persistence.Models;

namespace Timesheets.Web.Persistence
{
	public class TimesheetDbContext(DbContextOptions<TimesheetDbContext> options) : DbContext(options)
	{
		public DbSet<Timesheet> Timesheets { get; set; }
		public DbSet<Project> Projects { get; set; }
	}
}
