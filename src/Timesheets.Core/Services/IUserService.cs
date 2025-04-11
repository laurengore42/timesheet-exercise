using Timesheets.Core.Persistence.Models;

namespace Timesheets.Core.Services
{
    public interface IUserService
	{
		public ServiceResponse AddUser(User user);
        public ServiceResponse DeleteUser(User user);
    }
}
