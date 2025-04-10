using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;

namespace Timesheets.Tests
{
	public static class TestingHelper
	{
		public static readonly Project[] SampleProjects = [
			new()
			{
				Id = 1000,
				Name = "Project Alpha"
			},
			new()
			{
				Id = 1001,
				Name = "Project Beta"
			},
			new()
			{
				Id = 1002,
				Name = "Project Gamma"
			}
		];

		public static readonly Person[] SamplePersons = [
			new()
			{
				Id = 2000,
				Name = "John Smith"
			},
			new()
			{
				Id = 2001,
				Name = "Jane Doe"
			}
		];

		public static void DoDbContextSetup(Mock<TimesheetDbContext> ctxMock, Mock<DbSet<Timesheet>> timesheetsSet, Mock<DbSet<Person>> personsSet, Mock<DbSet<Project>> projectsSet)
		{
			ctxMock.Setup(x => x.Timesheets).ReturnsDbSet([], timesheetsSet);
			ctxMock.Setup(x => x.Persons).ReturnsDbSet(SeedingHelper.SamplePersons, personsSet);
			ctxMock.Setup(x => x.Projects).ReturnsDbSet(SeedingHelper.SampleProjects, projectsSet);
		}
	}
}
