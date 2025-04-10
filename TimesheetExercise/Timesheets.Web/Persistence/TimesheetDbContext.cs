using Microsoft.EntityFrameworkCore;
using Timesheets.Web.Persistence.Models;

namespace Timesheets.Web.Persistence
{
	public class TimesheetDbContext : DbContext
	{
		protected override void OnConfiguring
	   (DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase(databaseName: "Timesheets");
		}

		public DbSet<TimesheetDto> Timesheets { get; set; }
	}
}
