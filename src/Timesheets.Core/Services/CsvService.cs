

using System.Formats.Asn1;
using System.Globalization;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Timesheets.Core.Persistence;

namespace Timesheets.Core.Services
{
    public class CsvService(TimesheetDbContext ctx, ITimesheetService timesheetService) : ICsvService
	{
        public void CsvTimesheetExport()
		{
			if (ctx.Timesheets is null)
			{
				throw new InvalidOperationException("Could not access database tables");
            }

            var records = timesheetService.FetchAllTimesheets();

            using (TextWriter writer = new StreamWriter(@"wwwroot\export.csv", false))
            {
                var csv = new CsvWriter(writer, CultureInfo.CurrentCulture);
                csv.WriteRecords(records);
            }
        }
    }
}
