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

		public static readonly User[] SampleUsers = [
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
                UserId = 2000,
                User = SampleUsers.FirstOrDefault(x => x.Id == 2000),
                ProjectId = 1000,
                Project = SampleProjects.FirstOrDefault(x => x.Id == 1000),
                Date = new DateOnly(2014, 10, 22),
                Memo = "Developed new feature X",
                Hours = 4
            },
            new()
            {
                UserId = 2000,
                User = SampleUsers.FirstOrDefault(x => x.Id == 2000),
                ProjectId = 1001,
                Project = SampleProjects.FirstOrDefault(x => x.Id == 1001),
                Date = new DateOnly(2014, 10, 22),
                Memo = "Fixed bugs in module Y",
                Hours = 4
            },
            new()
            {
                UserId = 2001,
                User = SampleUsers.FirstOrDefault(x => x.Id == 2001),
                ProjectId = 1002,
                Project = SampleProjects.FirstOrDefault(x => x.Id == 1002),
                Date = new DateOnly(2014, 10, 22),
                Memo = "Conducted user testing",
                Hours = 6
            }
        ];

        public static void DoDbContextSetup(Mock<TimesheetDbContext> ctxMock, Mock<DbSet<Timesheet>> timesheetsSet, Mock<DbSet<User>> usersSet, Mock<DbSet<Project>> projectsSet)
		{
			ctxMock.Setup(x => x.Timesheets).ReturnsDbSet(SampleTimesheets, timesheetsSet);
			ctxMock.Setup(x => x.Users).ReturnsDbSet(SampleUsers, usersSet);
			ctxMock.Setup(x => x.Projects).ReturnsDbSet(SampleProjects, projectsSet);
		}
	}
}
