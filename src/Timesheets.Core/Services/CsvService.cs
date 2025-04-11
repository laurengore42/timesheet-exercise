using System.Globalization;
using CsvHelper;

namespace Timesheets.Core.Services
{
    public class CsvService(ITimesheetService timesheetService) : ICsvService
	{
        public void CsvTimesheetExport(string path)
		{
            var records = timesheetService.FetchAllTimesheets(true);

            using (TextWriter writer = new StreamWriter(path, false))
            {
                var csv = new CsvWriter(writer, CultureInfo.CurrentCulture);
                csv.WriteRecords(records);
            }
        }
    }
}
