using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;

namespace Timesheets.Core.Services
{
    public class UserService(TimesheetDbContext ctx) : IUserService
	{
		public bool AddUser(User user)
		{
			ctx.Users.Add(user);
			ctx.SaveChanges();

			return true;
		}
    }
}
