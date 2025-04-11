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

        public static readonly Timesheet[] SampleTimesheets = [
            new()
            {
                Id = 3001,
                PersonId = 1,
                ProjectId = 1,
                Date = new DateOnly(2014, 10, 22),
                Memo = "Developed new feature X",
                Hours = 4
            },
            new()
            {
                Id = 3002,
                PersonId = 1,
                ProjectId = 2,
                Date = new DateOnly(2014, 10, 22),
                Memo = "Fixed bugs in module Y",
                Hours = 4
            },
            new()
            {
                Id = 3003,
                PersonId = 2,
                ProjectId = 3,
                Date = new DateOnly(2014, 10, 22),
                Memo = "Conducted user testing",
                Hours = 6
            }
        ];

        public static void DoDbContextSetup(Mock<TimesheetDbContext> ctxMock, Mock<DbSet<Timesheet>> timesheetsSet, Mock<DbSet<Person>> personsSet, Mock<DbSet<Project>> projectsSet)
		{
			ctxMock.Setup(x => x.Timesheets).ReturnsDbSet(SampleTimesheets, timesheetsSet);
			ctxMock.Setup(x => x.Persons).ReturnsDbSet(SamplePersons, personsSet);
			ctxMock.Setup(x => x.Projects).ReturnsDbSet(SampleProjects, projectsSet);
		}
	}
}
