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
			var personsSet = new Mock<DbSet<Person>>();
			var projectsSet = new Mock<DbSet<Project>>();
            TestingHelper.DoDbContextSetup(ctxMock, timesheetsSet, personsSet, projectsSet);

			Timesheet newTimesheet = new()
			{
				PersonId = 0,
				ProjectId = 0,
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
			var personsSet = new Mock<DbSet<Person>>();
			var projectsSet = new Mock<DbSet<Project>>();
			TestingHelper.DoDbContextSetup(ctxMock, timesheetsSet, personsSet, projectsSet);

			Timesheet newTimesheet = new()
			{
				PersonId = 0,
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
		public void AddTimesheetWithInvalidPersonId()
		{
			// Arrange

			var ctxMock = new Mock<TimesheetDbContext>();
			var timesheetsSet = new Mock<DbSet<Timesheet>>();
			var personsSet = new Mock<DbSet<Person>>();
			var projectsSet = new Mock<DbSet<Project>>();
			TestingHelper.DoDbContextSetup(ctxMock, timesheetsSet, personsSet, projectsSet);

			Timesheet newTimesheet = new()
			{
				PersonId = -1,
				ProjectId = 0,
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
            var personsSet = new Mock<DbSet<Person>>();
            var projectsSet = new Mock<DbSet<Project>>();
            TestingHelper.DoDbContextSetup(ctxMock, timesheetsSet, personsSet, projectsSet);

            var timesheetService = new TimesheetService(ctxMock.Object);

            // Act

            var result = timesheetService.FetchAllTimesheets();
            var firstResult = result.FirstOrDefault(t => t.PersonName == "John Smith" && t.Date == "22/10/2014");

            // Assert

            Assert.NotNull(firstResult);
            Assert.Equal(8, firstResult.TotalHours);
        }
    }
}
