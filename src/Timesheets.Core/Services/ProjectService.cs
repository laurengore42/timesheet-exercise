using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;

namespace Timesheets.Core.Services
{
    public class ProjectService(TimesheetDbContext ctx) : IProjectService
	{
		public ServiceResponse AddProject(Project project)
		{
			ctx.Projects.Add(project);
			ctx.SaveChanges();

            return new ServiceResponse()
            {
                Success = true
            };
        }

        public ServiceResponse DeleteProject(Project project)
        {
            if (ctx.Timesheets.Any(t => t.ProjectId == project.Id))
            {
                return new ServiceResponse()
                {
                    Success = false,
                    Message = "Cannot delete a project with existing timesheet rows"
                };
            }

            ctx.Projects.Remove(project);
            ctx.SaveChanges();

            return new ServiceResponse()
            {
                Success = true
            };
        }
    }
}
