using Microsoft.EntityFrameworkCore;
using Timesheets.Core.Persistence.Models;

namespace Timesheets.Core.Persistence
{
	public static class SeedingHelper
	{
		public static readonly Project[] SampleProjects = [
			new()
			{
				Name = "Project Alpha"
			},
			new()
			{
				Name = "Project Beta"
			},
			new()
			{
				Name = "Project Gamma"
			}
		];

		public static readonly User[] SampleUsers = [
			new()
			{
				Name = "John Smith"
			},
			new()
			{
				Name = "Jane Doe"
			}
		];

		public static readonly Timesheet[] SampleTimesheets = [
			new()
			{
				UserId = 1,
				ProjectId = 1,
				Date = new DateOnly(2014, 10, 22),
				Memo = "Developed new feature X",
				Hours = 4
			},
			new()
			{
				UserId = 1,
				ProjectId = 2,
				Date = new DateOnly(2014, 10, 22),
				Memo = "Fixed bugs in module Y",
				Hours = 4
			},
			new()
			{
				UserId = 2,
				ProjectId = 3,
				Date = new DateOnly(2014, 10, 22),
				Memo = "Conducted user testing",
				Hours = 6
			}
		];

		public static void SeedDatabase(DbContext ctx)
		{
			if (ctx.Set<Project>().FirstOrDefault(t => t.Name == SampleProjects.First().Name) is null)
			{
				ctx.Set<Project>().AddRange(SampleProjects);
			}

			if (ctx.Set<User>().FirstOrDefault(t => t.Name == SampleUsers.First().Name) is null)
			{
				ctx.Set<User>().AddRange(SampleUsers);
			}

			if (ctx.Set<Timesheet>().FirstOrDefault(t => t.Hours == SampleTimesheets.First().Hours) is null)
			{
				ctx.Set<Timesheet>().AddRange(SampleTimesheets);
			}

			ctx.SaveChanges();
		}
	}
}
