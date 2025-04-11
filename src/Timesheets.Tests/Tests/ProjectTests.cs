using Microsoft.EntityFrameworkCore;
using Moq;
using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;
using Timesheets.Core.Services;
using Timesheets.Tests.Helpers;

namespace Timesheets.Tests.Tests
{
    public class ProjectTests
    {
        [Fact]
        public void AddThenDeleteAProject()
        {
            // Arrange

            var ctxMock = new Mock<TimesheetDbContext>();
            var projectsSet = new Mock<DbSet<Project>>();
            TestingHelper.SetupDbContext(ctxMock, projectsSet: projectsSet);

            var projectService = new ProjectService(ctxMock.Object);
            var newProject = new Project()
            {
                Name = "Project Omega"
            };

            // Act

            var addResult = projectService.AddProject(newProject);
            var deleteResult = projectService.DeleteProject(newProject);

            // Assert

            Assert.True(addResult.Success);
            projectsSet.Verify(x => x.Add(newProject), Times.Once());
            Assert.True(deleteResult.Success);
            projectsSet.Verify(x => x.Remove(newProject), Times.Once());
            ctxMock.Verify(x => x.SaveChanges(), Times.Exactly(2));
        }

        [Fact]
        public void DeleteAProjectWithTimesheetRows()
        {
            // Arrange

            var ctxMock = new Mock<TimesheetDbContext>();
            var projectsSet = new Mock<DbSet<Project>>();
            TestingHelper.SetupDbContext(ctxMock, projectsSet: projectsSet);

            var projectService = new ProjectService(ctxMock.Object);

            // Act

            var result = projectService.DeleteProject(TestingHelper.SampleProjects.First());

            // Assert

            Assert.False(result.Success);
            projectsSet.Verify(x => x.Remove(TestingHelper.SampleProjects.First()), Times.Never());
        }
    }
}
