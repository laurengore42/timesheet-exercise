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

            Assert.False(result);
            timesheetsSet.Verify(x => x.Add(It.Is<Timesheet>(t => t == newTimesheet)), Times.Never());
			ctxMock.Verify(x => x.SaveChanges(), Times.Never());
		}

        [Fact]
        public void GetTotalHoursForTimesheets()
        {
            // Arrange

            var ctxMock = new Mock<TimesheetDbContext>();
            TestingHelper.SetupDbContext(ctxMock);

            var timesheetService = new TimesheetService(ctxMock.Object);

            // Act

            var result = timesheetService.FetchAllTimesheets();

            // Assert

            var firstResult = result.FirstOrDefault(t => t.UserName == TestingHelper.SampleUsers.First().Name && t.Date == "22/10/2014");
            Assert.NotNull(firstResult);
            Assert.Equal(4, firstResult.Hours);
            Assert.Equal(8, firstResult.TotalHours);
        }

        [Fact]
        public void StripCommasForCsvExport()
        {
            // Arrange

            Timesheet newTimesheet = new()
            {
                UserId = TestingHelper.SampleUsers.First().Id,
                User = TestingHelper.SampleUsers.First(),
                ProjectId = TestingHelper.SampleProjects.First().Id,
                Project = TestingHelper.SampleProjects.First(),
                Date = new DateOnly(2014, 10, 22),
                Memo = "Test,words",
                Hours = TestingHelper.SampleTimesheets.First().Hours
            };

            // Act

            var plainResult = new TimesheetViewModel(newTimesheet, 8);
            var strippedResult = new TimesheetViewModel(newTimesheet, 8, true);

            // Assert

            Assert.Equal("Test,words", plainResult.Memo);
            Assert.Equal("Test words", strippedResult.Memo);
        }

        [Fact]
        public void VerifyCsvExport()
        {
            // Arrange

            var ctxMock = new Mock<TimesheetDbContext>();
            TestingHelper.SetupDbContext(ctxMock);

            var timesheetService = new TimesheetService(ctxMock.Object);
            var csvService = new CsvService(timesheetService);
            var csvPath = "test-export.csv";

            // Act

            csvService.CsvTimesheetExport(csvPath);

            // Assert

            string path = csvPath;
            using (StreamReader sr = File.OpenText(path))
            {
                string fileContents = sr.ReadToEnd();
                Assert.NotNull(fileContents);
                var fileLines = fileContents.Split("\r\n");
                Assert.Equal(TestingHelper.SampleTimesheets.Length + 2, fileLines.Length);
                var firstLine = fileLines[0];
                var secondLine = fileLines[1];
                Assert.Equal("UserId,UserName,Date,ProjectId,ProjectName,Memo,Hours,TotalHours", firstLine);
                Assert.Equal("2000,John Smith,22/10/2014,1000,Project Alpha,Developed new feature X,4,8", secondLine);
            }
        }
    }
}
