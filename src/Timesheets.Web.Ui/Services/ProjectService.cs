using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;

namespace Timesheets.Web.Ui.Services
{
    public class ProjectService(TimesheetDbContext ctx) : IProjectService
	{
		public bool AddProject(Project project)
		{
			if (ctx.Projects is null)
			{
				throw new InvalidOperationException("Could not access database tables");
			}

			ctx.Projects.Add(project);
			ctx.SaveChanges();

			return true;
		}
    }
}
