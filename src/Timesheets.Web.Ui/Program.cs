using Microsoft.EntityFrameworkCore;
using Timesheets.Web.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TimesheetDbContext>(optionsBuilder =>
{
	optionsBuilder.UseInMemoryDatabase(databaseName: "Timesheets");
	optionsBuilder.UseSeeding((context, _) =>
	{
		SeedingHelper.SeedDatabase(context);
	});
});

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
