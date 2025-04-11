using Microsoft.EntityFrameworkCore;
using Moq;
using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;
using Timesheets.Core.Services;
using Timesheets.Tests.Helpers;

namespace Timesheets.Tests.Tests
{
    public class TimesheetTests
    {
        [Fact]
        public void AddThenDeleteATimesheet()
        {
            // Arrange

            var ctxMock = new Mock<TimesheetDbContext>();
            var timesheetsSet = new Mock<DbSet<Timesheet>>();
            TestingHelper.SetupDbContext(ctxMock, timesheetsSet: timesheetsSet);

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

            var addResult = timesheetService.AddTimesheet(newTimesheet);
            var deleteResult = timesheetService.DeleteTimesheet(newTimesheet);

            // Assert

            Assert.True(addResult.Success);
            timesheetsSet.Verify(x => x.Add(newTimesheet), Times.Once());
            Assert.True(deleteResult.Success);
            timesheetsSet.Verify(x => x.Remove(newTimesheet), Times.Once());
            ctxMock.Verify(x => x.SaveChanges(), Times.Exactly(2));
        }

        [Fact]
        public void AddTimesheetWithInvalidProjectId()
        {
			// Arrange

			var ctxMock = new Mock<TimesheetDbContext>();
			var timesheetsSet = new Mock<DbSet<Timesheet>>();
            TestingHelper.SetupDbContext(ctxMock, timesheetsSet: timesheetsSet);

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
            timesheetsSet.Verify(x => x.Add(newTimesheet), Times.Never());
            ctxMock.Verify(x => x.SaveChanges(), Times.Never());
		}

		[Fact]
		public void AddTimesheetWithInvalidUserId()
		{
			// Arrange

			var ctxMock = new Mock<TimesheetDbContext>();
			var timesheetsSet = new Mock<DbSet<Timesheet>>();
            TestingHelper.SetupDbContext(ctxMock, timesheetsSet: timesheetsSet);

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
            timesheetsSet.Verify(x => x.Add(newTimesheet), Times.Never());
			ctxMock.Verify(x => x.SaveChanges(), Times.Never());
		}
    }
}
