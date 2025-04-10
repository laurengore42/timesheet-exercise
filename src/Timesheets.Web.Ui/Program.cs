using Microsoft.EntityFrameworkCore;
using Timesheets.Core.Persistence;
using Timesheets.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TimesheetDbContext>(optionsBuilder =>
{
    optionsBuilder.UseInMemoryDatabase(databaseName: "Timesheets");
    optionsBuilder.EnableSensitiveDataLogging();
    optionsBuilder.UseSeeding((context, _) =>
    {
        SeedingHelper.SeedDatabase(context);
    });
});
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ITimesheetService, TimesheetService>();
builder.Services.AddScoped<ICsvService, CsvService>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
TimesheetDbContext dbcontext = scope.ServiceProvider.GetRequiredService<TimesheetDbContext>();
dbcontext.Database.EnsureCreated();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
