using Timesheets.Core.Persistence.Models;

namespace Timesheets.Core.ViewModels
{
    public class TimesheetViewModel(Timesheet t, decimal totalHours, bool stripCommas = false)
    {
        public int Id { get; } = t.Id;
        public int UserId { get; } = t.UserId;
        public string UserName { get; } = (stripCommas ? t.User?.Name.Replace(",", " ") : t.User?.Name)
            ?? throw new ArgumentNullException(nameof(t.User));
        public string Date { get; } = t.Date.ToString("dd/MM/yyyy");
        public int ProjectId { get; } = t.ProjectId;
        public string ProjectName { get; } = (stripCommas ? t.Project?.Name.Replace(",", " ") : t.Project?.Name)
            ?? throw new ArgumentNullException(nameof(t.Project));
        public string Memo { get; } = stripCommas ? t.Memo.Replace(",", " ") : t.Memo;
        public decimal Hours { get; } = t.Hours;
        public decimal TotalHours { get; } = totalHours;
    }
}
