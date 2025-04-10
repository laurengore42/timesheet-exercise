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

		public static readonly Person[] SamplePersons = [
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
				PersonId = 1,
				ProjectId = 1,
				Date = new DateOnly(2014, 10, 22),
				Memo = "Developed new feature X",
				Hours = 4
			},
			new()
			{
				PersonId = 1,
				ProjectId = 2,
				Date = new DateOnly(2014, 10, 22),
				Memo = "Fixed bugs in module Y",
				Hours = 4
			},
			new()
			{
				PersonId = 2,
				ProjectId = 3,
				Date = new DateOnly(2014, 10, 22),
				Memo = "Conducted user testing",
				Hours = 6
			}
		];

		public static void SeedDatabase(DbContext context)
		{
			if (context.Set<Project>().FirstOrDefault(t => t.Name == "Project Alpha") is null)
			{
				context.Set<Project>().AddRange(SampleProjects);
			}

			if (context.Set<Person>().FirstOrDefault(t => t.Name == "John Smith") is null)
			{
				context.Set<Person>().AddRange(SamplePersons);
			}

			if (context.Set<Timesheet>().FirstOrDefault(t => t.Hours == 4) is null)
			{
				context.Set<Timesheet>().AddRange(SampleTimesheets);
			}

			context.SaveChanges();
		}
	}
}
