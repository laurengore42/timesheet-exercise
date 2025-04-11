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

			if (projectService.AddProject(project))
			{
				return RedirectToAction("Add", "Timesheet");
            }
            else
            {
                ViewBag.Error = "Failed to save project";
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

            if (ctx.Timesheets.Any(t => t.ProjectId == projectId))
            {
                ViewBag.Error = "Cannot delete a project with existing timesheet rows";
                return View();
            }

            if (projectService.DeleteProject(project))
            {
                return RedirectToAction("Add", "Timesheet");
            }
            else
            {
                throw new InvalidOperationException("Failed to delete project");
            }
        }
    }
}
