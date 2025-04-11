using Timesheets.Core.Persistence.Models;

namespace Timesheets.Core.Services
{
    public interface IProjectService
	{
		public ServiceResponse AddProject(Project project);
        public ServiceResponse DeleteProject(Project project);
    }
}
