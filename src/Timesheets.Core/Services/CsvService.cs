

using System.Formats.Asn1;
using System.Globalization;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Core.Persistence;

namespace Timesheets.Core.Services
{
    public class CsvService(TimesheetDbContext ctx, ITimesheetService timesheetService) : ICsvService
	{
        public IActionResult CsvTimesheetExport()
		{
			if (ctx.Timesheets is null)
			{
				throw new InvalidOperationException("Could not access database tables");
            }

            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream);
            var csvWriter = new CsvWriter(streamWriter, CultureInfo.CurrentCulture);
            csvWriter.WriteRecords(timesheetService.FetchAllTimesheets());

            return new FileStreamResult(memoryStream, "text/csv");
        }
    }
}
