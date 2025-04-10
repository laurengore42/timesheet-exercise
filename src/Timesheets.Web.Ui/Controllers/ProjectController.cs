using Microsoft.AspNetCore.Mvc;
using Timesheets.Core.Persistence.Models;
using Timesheets.Core.Services;

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

			// TO DO handle a No here
			// TO DO display validation for modelstate invalid?
			if (projectService.AddProject(project))
			{
				return RedirectToAction("Add", "Timesheet");
			}

			return View(project);
        }
    }
}
