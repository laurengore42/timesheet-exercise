﻿using Microsoft.AspNetCore.Mvc;

namespace Timesheets.Core.Services
{
    public interface ICsvService
	{
        public void CsvTimesheetExport(string path);
    }
}
