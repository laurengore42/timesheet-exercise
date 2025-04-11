using Microsoft.EntityFrameworkCore;
using Moq;
using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;
using Timesheets.Core.Services;
using Timesheets.Tests.Helpers;

namespace Timesheets.Tests.Tests
{
    public class UserTests
    {
        [Fact]
        public void AddThenDeleteAUser()
        {
            // Arrange

            var ctxMock = new Mock<TimesheetDbContext>();
            var usersSet = new Mock<DbSet<User>>();
            TestingHelper.SetupDbContext(ctxMock, usersSet: usersSet);

            var userService = new UserService(ctxMock.Object);
            var newUser = new User()
            {
                Name = "Bob Test"
            };

            // Act

            var addResult = userService.AddUser(newUser);
            var deleteResult = userService.DeleteUser(newUser);

            // Assert

            Assert.True(addResult.Success);
            usersSet.Verify(x => x.Add(newUser), Times.Once());
            Assert.True(deleteResult.Success);
            usersSet.Verify(x => x.Remove(newUser), Times.Once());
            ctxMock.Verify(x => x.SaveChanges(), Times.Exactly(2));
        }

        [Fact]
        public void DeleteAUserWithTimesheetRows()
        {
            // Arrange

            var ctxMock = new Mock<TimesheetDbContext>();
            var usersSet = new Mock<DbSet<User>>();
            TestingHelper.SetupDbContext(ctxMock, usersSet: usersSet);

            var userService = new UserService(ctxMock.Object);

            // Act

            var result = userService.DeleteUser(TestingHelper.SampleUsers.First());

            // Assert

            Assert.False(result.Success);
            usersSet.Verify(x => x.Remove(TestingHelper.SampleUsers.First()), Times.Never());
        }
    }
}
