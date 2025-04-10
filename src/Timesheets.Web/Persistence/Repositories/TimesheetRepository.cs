using System;
using Timesheets.Web.Persistence.Models;

namespace Timesheets.Web.Persistence.Repositories
{
	public class TimesheetRepository : ITimesheetRepository
	{
		public TimesheetRepository()
		{
			var context = new TimesheetDbContext();
			var sampleTimesheetDtos = new List<TimesheetDto>
				{
					new()
					{
						Id = Guid.NewGuid(),
						Name = "John Smith",
						Project = "Project Alpha",
						Date = new DateOnly(2014, 10, 22),
						Memo = "Developed new feature X",
						Hours = 4
					},
					new()
					{
						Id = Guid.NewGuid(),
						Name = "John Smith",
						Project = "Project Beta",
						Date = new DateOnly(2014, 10, 22),
						Memo = "Fixed bugs in module Y",
						Hours = 4
					},
					new()
					{
						Id = Guid.NewGuid(),
						Name = "Jane Doe",
						Project = "Project Gamma",
						Date = new DateOnly(2014, 10, 22),
						Memo = "Conducted user testing",
						Hours = 6
					}
				};

			if (!context.Timesheets.Any())
			{
				context.Timesheets.AddRange(sampleTimesheetDtos);
				context.SaveChanges();
			}
		}

		public IEnumerable<TimesheetDto> GetTimesheets()
		{
			var context = new TimesheetDbContext();
			return context.Timesheets;
		}
	}
}
