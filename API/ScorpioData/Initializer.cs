using System;
using ScorpioData.Models;

namespace ScorpioData
{
	public static class Initializer
	{
		public static void Initialize(Context context)
		{
			context.Database.EnsureCreated();

			if (context.Users.Any())
            {
				return;
            }

			var seedUsers = new User[]
			{
				new User { Name = "Jenna Stannis" },
				new User { Name = "Kerr Avon" },
				new User { Name = "Servalan" },
				new User { Name = "Vila Restal" },
			};
			context.Users.AddRange(seedUsers);
			context.SaveChanges();
		}
	}
}

