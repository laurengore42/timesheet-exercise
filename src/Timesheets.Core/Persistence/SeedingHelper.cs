using Microsoft.EntityFrameworkCore;
using Timesheets.Core.Persistence.Models;

namespace Timesheets.Core.Persistence
{
    public static class SeedingHelper
    {
        public static void SeedDatabase(DbContext context)
        {
            var sampleProjects = new List<Project>
                {
                    new()
                    {
                        Name = "Project Alpha"
                    },
                    new()
                    {
                        Name = "Project Beta"
                    },
                    new()
                    {
                        Name = "Project Gamma"
                    }
                };

            var testProject = context.Set<Project>().FirstOrDefault(t => t.Name == "Project Alpha");
            if (testProject == null)
            {
                context.Set<Project>().AddRange(sampleProjects);
            }

            var samplePersons = new List<Person>
                {
                    new()
                    {
                        Name = "John Smith",
                    },
                    new()
                    {
                        Name = "Jane Doe",
                    }
                };

            var testPerson = context.Set<Person>().FirstOrDefault(t => t.Name == "Jane Doe");
            if (testPerson == null)
            {
                context.Set<Person>().AddRange(samplePersons);
            }

            context.SaveChanges();

            var sampleTimesheets = new List<Timesheet>
                {
                    new()
                    {
                        PersonId = context.Set<Person>().First(p => p.Name == "John Smith").Id,
                        ProjectId = context.Set<Project>().First(p => p.Name == "Project Alpha").Id,
                        Date = new DateOnly(2014, 10, 22),
                        Memo = "Developed new feature X",
                        Hours = 4
                    },
                    new()
                    {
                        PersonId = context.Set<Person>().First(p => p.Name == "John Smith").Id,
                        ProjectId = context.Set<Project>().First(p => p.Name == "Project Beta").Id,
                        Date = new DateOnly(2014, 10, 22),
                        Memo = "Fixed bugs in module Y",
                        Hours = 4
                    },
                    new()
                    {
                        PersonId = context.Set<Person>().First(p => p.Name == "Jane Doe").Id,
                        ProjectId = context.Set<Project>().First(p => p.Name == "Project Gamma").Id,
                        Date = new DateOnly(2014, 10, 22),
                        Memo = "Conducted user testing",
                        Hours = 6
                    }
                };

            var testTimesheet = context.Set<Timesheet>().FirstOrDefault(t => t.Hours == 4);
            if (testTimesheet == null)
            {
                context.Set<Timesheet>().AddRange(sampleTimesheets);
            }

            context.SaveChanges();
        }
    }
}
