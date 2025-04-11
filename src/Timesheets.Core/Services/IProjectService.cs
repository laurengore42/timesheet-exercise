using Timesheets.Core.Persistence.Models;

namespace Timesheets.Core.Services
{
    public interface IProjectService
	{
		public bool AddProject(Project project);
        public bool DeleteProject(Project project);
    }
}
