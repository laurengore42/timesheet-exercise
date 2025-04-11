using Microsoft.EntityFrameworkCore;
using Moq;
using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;
using Timesheets.Core.Services;
using Timesheets.Core.ViewModels;

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
			var usersSet = new Mock<DbSet<User>>();
			var projectsSet = new Mock<DbSet<Project>>();
            TestingHelper.DoDbContextSetup(ctxMock, timesheetsSet, usersSet, projectsSet);

			Timesheet newTimesheet = new()
			{
				UserId = 2000,
				ProjectId = 1000,
				Date = new DateOnly(2014, 10, 22),
				Memo = "Developed new feature X",
				Hours = 4
			};

			var timesheetService = new TimesheetService(ctxMock.Object);

            // Act

            var result = timesheetService.AddTimesheet(newTimesheet);

            // Assert

            Assert.True(result);
			timesheetsSet.Verify(x => x.Add(It.Is<Timesheet>(t => t == newTimesheet)), Times.Once());
            ctxMock.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Fact]
        public void AddTimesheetWithInvalidProjectId()
        {
			// Arrange

			var ctxMock = new Mock<TimesheetDbContext>();
			var timesheetsSet = new Mock<DbSet<Timesheet>>();
			var usersSet = new Mock<DbSet<User>>();
			var projectsSet = new Mock<DbSet<Project>>();
			TestingHelper.DoDbContextSetup(ctxMock, timesheetsSet, usersSet, projectsSet);

			Timesheet newTimesheet = new()
			{
				UserId = 2000,
				ProjectId = -1,
				Date = new DateOnly(2014, 10, 22),
				Memo = "Developed new feature X",
				Hours = 4
			};

			var timesheetService = new TimesheetService(ctxMock.Object);

            // Act

            var result = timesheetService.AddTimesheet(newTimesheet);

            // Assert

            Assert.False(result);
            timesheetsSet.Verify(x => x.Add(It.Is<Timesheet>(t => t == newTimesheet)), Times.Never());
            ctxMock.Verify(x => x.SaveChanges(), Times.Never());
		}

		[Fact]
		public void AddTimesheetWithInvalidUserId()
		{
			// Arrange

			var ctxMock = new Mock<TimesheetDbContext>();
			var timesheetsSet = new Mock<DbSet<Timesheet>>();
			var usersSet = new Mock<DbSet<User>>();
			var projectsSet = new Mock<DbSet<Project>>();
			TestingHelper.DoDbContextSetup(ctxMock, timesheetsSet, usersSet, projectsSet);

			Timesheet newTimesheet = new()
			{
				UserId = -1,
                ProjectId = 1000,
                Date = new DateOnly(2014, 10, 22),
				Memo = "Developed new feature X",
				Hours = 4
			};

			var timesheetService = new TimesheetService(ctxMock.Object);

			// Act

			var result = timesheetService.AddTimesheet(newTimesheet);

            // Assert

            Assert.False(result);
            timesheetsSet.Verify(x => x.Add(It.Is<Timesheet>(t => t == newTimesheet)), Times.Never());
			ctxMock.Verify(x => x.SaveChanges(), Times.Never());
		}

        [Fact]
        public void GetTotalHoursForTimesheets()
        {
            // Arrange

            var ctxMock = new Mock<TimesheetDbContext>();
            var timesheetsSet = new Mock<DbSet<Timesheet>>();
            var usersSet = new Mock<DbSet<User>>();
            var projectsSet = new Mock<DbSet<Project>>();
            TestingHelper.DoDbContextSetup(ctxMock, timesheetsSet, usersSet, projectsSet);

            var timesheetService = new TimesheetService(ctxMock.Object);

            // Act

            var result = timesheetService.FetchAllTimesheets();

            // Assert

            var firstResult = result.FirstOrDefault(t => t.UserName == "John Smith" && t.Date == "22/10/2014");
            Assert.NotNull(firstResult);
            Assert.Equal(4, firstResult.Hours);
            Assert.Equal(8, firstResult.TotalHours);
        }

        [Fact]
        public void StripCommasForCsvExport()
        {
            // Arrange

            var ctxMock = new Mock<TimesheetDbContext>();
            var timesheetsSet = new Mock<DbSet<Timesheet>>();
            var usersSet = new Mock<DbSet<User>>();
            var projectsSet = new Mock<DbSet<Project>>();
            TestingHelper.DoDbContextSetup(ctxMock, timesheetsSet, usersSet, projectsSet);

            Timesheet newTimesheet = new()
            {
                UserId = 2000,
                User = TestingHelper.SampleUsers.First(u => u.Id == 2000),
                ProjectId = 1000,
                Project = TestingHelper.SampleProjects.First(p => p.Id == 1000),
                Date = new DateOnly(2014, 10, 22),
                Memo = "Test,words",
                Hours = 4
            };

            // Act

            var plainResult = new TimesheetViewModel(newTimesheet, 8);
            var strippedResult = new TimesheetViewModel(newTimesheet, 8, true);

            // Assert

            Assert.Equal("Test,words", plainResult.Memo);
            Assert.Equal("Test words", strippedResult.Memo);
        }
    }
}
