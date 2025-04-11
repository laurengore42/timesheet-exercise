using Microsoft.AspNetCore.Mvc;
using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;
using Timesheets.Core.Services;

namespace Timesheets.Web.Ui.Controllers
{
    public class ProjectController(TimesheetDbContext ctx, IProjectService projectService) : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Project project)
        {
            if (!ModelState.IsValid)
            {
                return View(project);
            }

            var resp = projectService.AddProject(project);

			if (resp.Success)
			{
				return RedirectToAction("Add", "Timesheet");
            }
            else
            {
                ViewBag.Error = resp.Message;
                return View(project);
            }
        }

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int projectId)
        {
            var project = ctx.Projects.Find(projectId)
                ?? throw new ArgumentOutOfRangeException(nameof(projectId));

            var resp = projectService.DeleteProject(project);

            if (resp.Success)
            {
                return RedirectToAction("Add", "Timesheet");
            }
            else
            {
                throw new InvalidOperationException(resp.Message);
            }
        }
    }
}
