using System;
using ScorpioData.Dtos;
using ScorpioData.Models;

namespace ScorpioData.Services
{
	public interface IUserService
    {
		public UserDto Me();
		public UserDto? GetUser(int userId);
    }

	public class UserService: IUserService
    {
		private readonly Context _context;

		public UserService(Context context)
		{
			_context = context;
		}

		public UserDto Me()
        {
			// Would extract user details from authentication code
			// But for now, I'm a random person every day, and I'm loving it

			var userCount = _context.Users.Count();
			var rando = new Random();
			var randomInt = rando.Next(1, userCount + 1);

			var user = _context.Users
				.First(u => u.Id == randomInt);
			return user.ToDto();
        }

        public UserDto? GetUser(int userId)
        {
			var user = _context.Users
				.FirstOrDefault(u => u.Id == userId);				;
			return user?.ToDto();
        }
    }
}

