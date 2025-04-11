using Timesheets.Core.Persistence;
using Timesheets.Core.Persistence.Models;

namespace Timesheets.Core.Services
{
    public class UserService(TimesheetDbContext ctx) : IUserService
	{
		public ServiceResponse AddUser(User user)
		{
			ctx.Users.Add(user);
			ctx.SaveChanges();

            return new ServiceResponse()
            {
                Success = true
            };
        }

        public ServiceResponse DeleteUser(User user)
        {
            if (ctx.Timesheets.Any(t => t.UserId == user.Id))
            {
                return new ServiceResponse()
                {
                    Success = false,
                    Message = "Cannot delete a user with existing timesheet rows"
                };
            }

            ctx.Users.Remove(user);
            ctx.SaveChanges();

            return new ServiceResponse()
            {
                Success = true
            };
        }
    }
}
