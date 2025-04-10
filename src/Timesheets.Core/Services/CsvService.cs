using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Timesheets.Core.Persistence;
using Timesheets.Core.ViewModels;

namespace Timesheets.Core.Services
{
    public class CsvService(TimesheetDbContext ctx) : ICsvService
	{
        public void Export()
		{
			if (ctx.Timesheets is null)
			{
				throw new InvalidOperationException("Could not access database tables");
			}

            var hoursPerPersonPerDay = ctx.Timesheets
                .GroupBy(t => new { t.PersonId, t.Date })
                .Select(group => new { group.Key, HourSum = group.Sum(g => g.Hours) })
                .ToList();

            var fullTimesheets = ctx.Timesheets
                .Include(t => t.Person)
                .Include(t => t.Project)
                .ToList()
                .Select(t => new CsvTimesheetViewModel(t,
                    hoursPerPersonPerDay.Find(
                        hours => hours.Key.PersonId == t.PersonId &&
                                 hours.Key.Date == t.Date)?
                        .HourSum ?? 0));

			using (var writer = new StreamWriter("assets\\export.csv"))
			using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(fullTimesheets);
			}
		}
    }
}
