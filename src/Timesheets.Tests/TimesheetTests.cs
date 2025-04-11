using Microsoft.EntityFrameworkCore;
using Moq;
using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;
using Timesheets.Core.Services;

namespace Timesheets.Tests
{
    public class TimesheetTests
    {
        [Fact]
        public void AddValidTimesheet()
        {
            // Arrange

            var ctxMock = new Mock<TimesheetDbContext>();
            var timesheetsSet = new Mock<DbSet<Timesheet>>();
            TestingHelper.SetupDbContext(ctxMock, timesheetsSet);

			Timesheet newTimesheet = new()
			{
				UserId = TestingHelper.SampleUsers.First().Id,
				ProjectId = TestingHelper.SampleProjects.First().Id,
                Date = TestingHelper.SampleTimesheets.First().Date,
                Memo = TestingHelper.SampleTimesheets.First().Memo,
                Hours = TestingHelper.SampleTimesheets.First().Hours
			};

			var timesheetService = new TimesheetService(ctxMock.Object);

            // Act

            var result = timesheetService.AddTimesheet(newTimesheet);

            // Assert

            Assert.True(result.Success);
			timesheetsSet.Verify(x => x.Add(It.Is<Timesheet>(t => t == newTimesheet)), Times.Once());
            ctxMock.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Fact]
        public void AddTimesheetWithInvalidProjectId()
        {
			// Arrange

			var ctxMock = new Mock<TimesheetDbContext>();
			var timesheetsSet = new Mock<DbSet<Timesheet>>();
			TestingHelper.SetupDbContext(ctxMock, timesheetsSet);

			Timesheet newTimesheet = new()
            {
                UserId = TestingHelper.SampleUsers.First().Id,
                ProjectId = -1,
                Date = TestingHelper.SampleTimesheets.First().Date,
                Memo = TestingHelper.SampleTimesheets.First().Memo,
                Hours = TestingHelper.SampleTimesheets.First().Hours
            };

			var timesheetService = new TimesheetService(ctxMock.Object);

            // Act

            var result = timesheetService.AddTimesheet(newTimesheet);

            // Assert

            Assert.False(result.Success);
            timesheetsSet.Verify(x => x.Add(It.Is<Timesheet>(t => t == newTimesheet)), Times.Never());
            ctxMock.Verify(x => x.SaveChanges(), Times.Never());
		}

		[Fact]
		public void AddTimesheetWithInvalidUserId()
		{
			// Arrange

			var ctxMock = new Mock<TimesheetDbContext>();
			var timesheetsSet = new Mock<DbSet<Timesheet>>();
			TestingHelper.SetupDbContext(ctxMock, timesheetsSet);

			Timesheet newTimesheet = new()
            {
                UserId = -1,
                ProjectId = TestingHelper.SampleProjects.First().Id,
                Date = TestingHelper.SampleTimesheets.First().Date,
                Memo = TestingHelper.SampleTimesheets.First().Memo,
                Hours = TestingHelper.SampleTimesheets.First().Hours
            };

			var timesheetService = new TimesheetService(ctxMock.Object);

			// Act

			var result = timesheetService.AddTimesheet(newTimesheet);

            // Assert

            Assert.False(result.Success);
            timesheetsSet.Verify(x => x.Add(It.Is<Timesheet>(t => t == newTimesheet)), Times.Never());
			ctxMock.Verify(x => x.SaveChanges(), Times.Never());
		}

        [Fact]
        public void DeleteAProjectWithTimesheetRows()
        {
            // Arrange

            var ctxMock = new Mock<TimesheetDbContext>();
            var projectsSet = new Mock<DbSet<Project>>();
            TestingHelper.SetupDbContext(ctxMock, null, projectsSet);

            var projectService = new ProjectService(ctxMock.Object);

            // Act

            var result = projectService.DeleteProject(TestingHelper.SampleProjects.First());

            // Assert

            Assert.False(result.Success);
            projectsSet.Verify(x => x.Remove(TestingHelper.SampleProjects.First()), Times.Never());
        }

        [Fact]
        public void DeleteAUserWithTimesheetRows()
        {
            // Arrange

            var ctxMock = new Mock<TimesheetDbContext>();
            var usersSet = new Mock<DbSet<User>>();
            TestingHelper.SetupDbContext(ctxMock, null, null, usersSet);

            var userService = new UserService(ctxMock.Object);

            // Act

            var result = userService.DeleteUser(TestingHelper.SampleUsers.First());

            // Assert

            Assert.False(result.Success);
            usersSet.Verify(x => x.Remove(TestingHelper.SampleUsers.First()), Times.Never());
        }
    }
}
