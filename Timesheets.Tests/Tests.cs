using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;
using Timesheets.Web.Ui.Services;

namespace Timesheets.Tests
{
    public class Tests
    {
		private static void DoDbContextSetup(Mock<TimesheetDbContext> ctxMock, Mock<DbSet<Timesheet>> timesheetsSet, Mock<DbSet<Person>> personsSet, Mock<DbSet<Project>> projectsSet)
		{
			ctxMock.Setup(x => x.Timesheets).ReturnsDbSet([], timesheetsSet);
			ctxMock.Setup(x => x.Persons).ReturnsDbSet(SeedingHelper.SamplePersons, personsSet);
			ctxMock.Setup(x => x.Projects).ReturnsDbSet(SeedingHelper.SampleProjects, projectsSet);
		}

        [Fact]
        public void AddValidTimesheet()
        {
            // Arrange

            var ctxMock = new Mock<TimesheetDbContext>();
            var timesheetsSet = new Mock<DbSet<Timesheet>>();
			var personsSet = new Mock<DbSet<Person>>();
			var projectsSet = new Mock<DbSet<Project>>();
            DoDbContextSetup(ctxMock, timesheetsSet, personsSet, projectsSet);

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

			result.Equals(true);
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
            DoDbContextSetup(ctxMock, timesheetsSet, personsSet, projectsSet);

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

            result.Equals(false);
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
            DoDbContextSetup(ctxMock, timesheetsSet, personsSet, projectsSet);

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

			result.Equals(false);
			timesheetsSet.Verify(x => x.Add(It.Is<Timesheet>(t => t == newTimesheet)), Times.Never());
			ctxMock.Verify(x => x.SaveChanges(), Times.Never());
		}
	}
}
