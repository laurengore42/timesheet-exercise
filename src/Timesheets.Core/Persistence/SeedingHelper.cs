using Microsoft.EntityFrameworkCore;
using Timesheets.Core.Persistence.Models;

namespace Timesheets.Core.Persistence
{
	public static class SeedingHelper
	{
		public static readonly Project[] SampleProjects = [
			new()
			{
				Id = 0,
				Name = "Project Alpha"
			},
			new()
			{
				Id = 1,
				Name = "Project Beta"
			},
			new()
			{
				Id = 2,
				Name = "Project Gamma"
			}
		];

		public static readonly Person[] SamplePersons = [
			new()
			{
				Id = 0,
				Name = "John Smith"
			},
			new()
			{
				Id = 1,
				Name = "Jane Doe"
			}
		];

		public static readonly Timesheet[] SampleTimesheets = [
			new()
			{
				PersonId = 0,
				ProjectId = 0,
				Date = new DateOnly(2014, 10, 22),
				Memo = "Developed new feature X",
				Hours = 4
			},
			new()
			{
				PersonId = 0,
				ProjectId = 1,
				Date = new DateOnly(2014, 10, 22),
				Memo = "Fixed bugs in module Y",
				Hours = 4
			},
			new()
			{
				PersonId = 1,
				ProjectId = 2,
				Date = new DateOnly(2014, 10, 22),
				Memo = "Conducted user testing",
				Hours = 6
			}
		];

		public static void SeedDatabase(DbContext context)
		{
			var testProject = context.Set<Project>().FirstOrDefault(t => t.Name == "Project Alpha");
			if (testProject == null)
			{
				context.Set<Project>().AddRange(SampleProjects);
			}

			var testPerson = context.Set<Person>().FirstOrDefault(t => t.Name == "Jane Doe");
			if (testPerson == null)
			{
				context.Set<Person>().AddRange(SamplePersons);
			}

			var testTimesheet = context.Set<Timesheet>().FirstOrDefault(t => t.Hours == 4);
			if (testTimesheet == null)
			{
				context.Set<Timesheet>().AddRange(SampleTimesheets);
			}

			context.SaveChanges();
		}
	}
}
