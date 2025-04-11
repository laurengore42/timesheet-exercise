using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;

namespace Timesheets.Core.Services
{
    public class ProjectService(TimesheetDbContext ctx) : IProjectService
	{
		public bool AddProject(Project project)
		{
			ctx.Projects.Add(project);
			ctx.SaveChanges();

			return true;
        }

        public bool DeleteProject(Project project)
        {
            ctx.Projects.Remove(project);
            ctx.SaveChanges();

            return true;
        }
    }
}
