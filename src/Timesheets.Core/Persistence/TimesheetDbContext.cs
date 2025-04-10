using Microsoft.EntityFrameworkCore;
using Timesheets.Core.Persistence.Models;

namespace Timesheets.Core.Persistence
{
    public class TimesheetDbContext : DbContext
	{
        public TimesheetDbContext() { }
		public TimesheetDbContext(DbContextOptions<TimesheetDbContext> options) : base(options) { }

		public virtual DbSet<Timesheet>? Timesheets { get; set; }
        public virtual DbSet<Project>? Projects { get; set; }
        public virtual DbSet<Person>? Persons { get; set; }
    }
}
