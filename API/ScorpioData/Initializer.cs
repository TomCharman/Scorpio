using System;
using Microsoft.EntityFrameworkCore;
using ScorpioData.Models;

namespace ScorpioData
{
	public static class Initializer
	{
		public static async Task Initialize(Context context)
		{
			await context.Database.MigrateAsync();

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
			await context.Users.AddRangeAsync(seedUsers);
			await context.SaveChangesAsync();
		}
	}
}

