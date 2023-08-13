using eTickets.data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace eTickets.data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Actors>()
				.HasMany(x => x.Movies)
				.WithMany(x => x.Actors)
				.UsingEntity<movieActor>(
					x => x.HasOne(x => x.Movie).WithMany(x => x.movieActors).HasForeignKey(x => x.MovieId),
					x => x.HasOne(x => x.Actor).WithMany(x => x.movieActors).HasForeignKey(x => x.ActorId),
					x => x.HasKey(x => new { x.ActorId, x.MovieId })
				);


			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Orders>()
				.HasMany(x => x.Movies)
				.WithMany(x => x.Orders)
				.UsingEntity<MovieOrder>(
					x => x.HasOne(x => x.Movie).WithMany(x => x.movieOrders).HasForeignKey(x => x.MovieId),
					x => x.HasOne(x => x.order).WithMany(x => x.movieOrders).HasForeignKey(x => x.orderId),
					x => x.HasKey(x => new { x.orderId, x.MovieId })
				);
		}
		public DbSet<Actors> Actors { get; set; }
        public DbSet<Directors> Directors { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Cinemas> Cinemas { get; set; }
        public DbSet<Categories> Catgerys { get; set; }
        public DbSet<Orders> Orders { get; set; }
		public DbSet<movieActor> movieActors { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<MovieOrder> MovieOrders { get; set; }
		public DbSet<MovieRating> MovieRatings { get; set; }
	}
}
