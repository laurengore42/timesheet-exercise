using Microsoft.EntityFrameworkCore;
using Timesheets.Web.Persistence.Models;

namespace Timesheets.Web.Persistence
{
	public static class SeedingHelper
	{
		public static void SeedDatabase(DbContext context)
		{
			var sampleProjects = new List<Project>
				{
					new()
					{
						Id = new Guid("366c31db-29c2-449e-bbe0-90ba0b131ffb"),
						Name = "Project Alpha"
					},
					new()
					{
						Id = new Guid("4c293bc3-d473-405d-a5f9-18f4acfe7d41"),
						Name = "Project Beta"
					},
					new()
					{
						Id = new Guid("230bda5e-3004-4690-bd4d-1127e4f3e994"),
						Name = "Project Gamma"
					}
				};

			var testProject = context.Set<Project>().FirstOrDefault(t => t.Name == "Project Alpha");
			if (testProject == null)
			{
				context.Set<Project>().AddRange(sampleProjects);
			}

			var sampleTimesheets = new List<Timesheet>
				{
					new()
					{
						Id = Guid.NewGuid(),
						Name = "John Smith",
						ProjectId = new Guid("366c31db-29c2-449e-bbe0-90ba0b131ffb"),
						Date = new DateOnly(2014, 10, 22),
						Memo = "Developed new feature X",
						Hours = 4
					},
					new()
					{
						Id = Guid.NewGuid(),
						Name = "John Smith",
						ProjectId = new Guid("4c293bc3-d473-405d-a5f9-18f4acfe7d41"),
						Date = new DateOnly(2014, 10, 22),
						Memo = "Fixed bugs in module Y",
						Hours = 4
					},
					new()
					{
						Id = Guid.NewGuid(),
						Name = "Jane Doe",
						ProjectId = new Guid("230bda5e-3004-4690-bd4d-1127e4f3e994"),
						Date = new DateOnly(2014, 10, 22),
						Memo = "Conducted user testing",
						Hours = 6
					}
				};

			var testTimesheet = context.Set<Timesheet>().FirstOrDefault(t => t.Name == "John Smith");
			if (testTimesheet == null)
			{
				context.Set<Timesheet>().AddRange(sampleTimesheets);
			}

			context.SaveChanges();
		}
	}
}
