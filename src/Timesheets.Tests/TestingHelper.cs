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
                UserId = SampleUsers[0].Id,
                User = SampleUsers[0],
                ProjectId = SampleProjects[0].Id,
                Project = SampleProjects[0],
                Date = new DateOnly(2014, 10, 22),
                Memo = "Developed new feature X",
                Hours = 4
            },
            new()
            {
                UserId = SampleUsers[0].Id,
                User = SampleUsers[0],
                ProjectId = SampleProjects[1].Id,
                Project = SampleProjects[1],
                Date = new DateOnly(2014, 10, 22),
                Memo = "Fixed bugs in module Y",
                Hours = 4
            },
            new()
            {
                UserId = SampleUsers[1].Id,
                User = SampleUsers[1],
                ProjectId = SampleProjects[2].Id,
                Project = SampleProjects[2],
                Date = new DateOnly(2014, 10, 22),
                Memo = "Conducted user testing",
                Hours = 6
            }
        ];

        public static void SetupDbContext(Mock<TimesheetDbContext> ctxMock, Mock<DbSet<Timesheet>>? timesheetsSet = null)
        {
            timesheetsSet ??= new Mock<DbSet<Timesheet>>();

            ctxMock.Setup(x => x.Timesheets).ReturnsDbSet(SampleTimesheets, timesheetsSet);
			ctxMock.Setup(x => x.Users).ReturnsDbSet(SampleUsers);
			ctxMock.Setup(x => x.Projects).ReturnsDbSet(SampleProjects);
		}
	}
}
