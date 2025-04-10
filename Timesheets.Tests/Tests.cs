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
        [Fact]
        public void AddValidTimesheet()
        {
            // Arrange

            var timesheetsSet = new Mock<DbSet<Timesheet>>();
            var ctxMock = new Mock<TimesheetDbContext>();
            ctxMock.Setup(x => x.Timesheets).Returns(timesheetsSet.Object);

            var date = DateTime.Now;
            var newTimesheet = SeedingHelper.SampleTimesheets.First();

            var timesheetService = new TimesheetService(ctxMock.Object);

            // Act
            timesheetService.AddTimesheet(newTimesheet);

            // Assert
            timesheetsSet.Verify(x => x.Add(It.Is<Timesheet>(t => t == newTimesheet)), Times.Once());
            ctxMock.Verify(x => x.SaveChanges(), Times.Once());
        }
    }
}
