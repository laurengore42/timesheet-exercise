using System.Globalization;
using CsvHelper;
using Timesheets.Core.Persistence;

namespace Timesheets.Core.Services
{
    public class CsvService(TimesheetDbContext ctx, ITimesheetService timesheetService) : ICsvService
	{
        public void CsvTimesheetExport()
		{
            var records = timesheetService.FetchAllTimesheets(true);

            using (TextWriter writer = new StreamWriter(@"wwwroot\export.csv", false))
            {
                var csv = new CsvWriter(writer, CultureInfo.CurrentCulture);
                csv.WriteRecords(records);
            }
        }
    }
}
