using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;
using Timesheets.Core.Services;

namespace Timesheets.Tests
{
    public class CsvTests
    {
        [Fact]
        public void ExportCsv()
        {
            // Arrange

            var ctxMock = new Mock<TimesheetDbContext>();
            var timesheetsSet = new Mock<DbSet<Timesheet>>();
			var personsSet = new Mock<DbSet<Person>>();
			var projectsSet = new Mock<DbSet<Project>>();
            TestingHelper.DoDbContextSetup(ctxMock, timesheetsSet, personsSet, projectsSet);

            var timesheetService = new TimesheetService(ctxMock.Object);
            var csvService = new CsvService(ctxMock.Object, timesheetService);

            // Act

            csvService.Export();

            // Assert

            // timesheetsSet.Verify(x => x.Select(), Times.Exactly(2));
        }
	}
}
