using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;

namespace Timesheets.Core.Services
{
    public class UserService(TimesheetDbContext ctx) : IUserService
	{
		public bool AddUser(User user)
		{
			if (ctx.Users is null)
			{
				throw new InvalidOperationException("Could not access database tables");
			}

			ctx.Users.Add(user);
			ctx.SaveChanges();

			return true;
		}
    }
}
