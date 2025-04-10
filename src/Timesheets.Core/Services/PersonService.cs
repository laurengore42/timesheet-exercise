using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;

namespace Timesheets.Core.Services
{
    public class PersonService(TimesheetDbContext ctx) : IPersonService
	{
		public bool AddPerson(Person person)
		{
			if (ctx.Persons is null)
			{
				throw new InvalidOperationException("Could not access database tables");
			}

			ctx.Persons.Add(person);
			ctx.SaveChanges();

			return true;
		}
    }
}
