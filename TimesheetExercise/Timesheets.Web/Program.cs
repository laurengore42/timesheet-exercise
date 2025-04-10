using Timesheets.Web.Persistence.Interfaces;
using Timesheets.Web.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITimesheetRepository, TimesheetRepository>();

var app = builder.Build();

app.MapGet("/", () => $"Hello World!");

app.Run();
