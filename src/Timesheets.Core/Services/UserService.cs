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

        public bool DeleteUser(User user)
        {
            ctx.Users.Remove(user);
            ctx.SaveChanges();

            return true;
        }
    }
}
