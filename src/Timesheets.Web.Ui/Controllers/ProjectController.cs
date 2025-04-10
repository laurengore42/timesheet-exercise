using Microsoft.AspNetCore.Mvc;
using Timesheets.Core.Persistence.Models;
using Timesheets.Web.Ui.Services;

namespace Timesheets.Web.Ui.Controllers
{
    public class ProjectController(IProjectService projectService) : Controller
    {
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public IActionResult Add(Project project)
        {
            if (!ModelState.IsValid)
            {
                return View(project);
            }

            if (projectService.AddProject(project))
			{
				return RedirectToAction("Add", "Timesheet");
			}

			return View(project);
        }
    }
}
