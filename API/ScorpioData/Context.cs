using System;
using ScorpioData.Models;
using Microsoft.EntityFrameworkCore;

namespace ScorpioData
{
	public class Context : DbContext
	{
		public Context(DbContextOptions<Context> options) : base(options)
		{ }

		public DbSet<Comment> Comments { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Vote> Votes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Comment>()
				.HasOne<Comment>(c => c.ParentComment)
				.WithMany(pc => pc.ChildComments)
				.HasForeignKey(c => c.ParentCommentId)
				.IsRequired(false);
        }

	}
}

