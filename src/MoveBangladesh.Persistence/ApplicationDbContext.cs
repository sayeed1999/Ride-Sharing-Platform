﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoveBangladesh.Domain.Entities;
using MoveBangladesh.Persistence.EntityConfigurations;

namespace MoveBangladesh.Persistence;

public class ApplicationDbContext : IdentityDbContext<User>
{
	private readonly IConfiguration configuration;

	public ApplicationDbContext(
		DbContextOptions<ApplicationDbContext> options,
		IConfiguration configuration) : base(options)
	{
		this.configuration = configuration;
	}

	#region dbsets
	public DbSet<Customer> Customers { get; set; }
	public DbSet<CustomerRating> CustomerRatings { get; set; }
	public DbSet<Driver> Drivers { get; set; }
	public DbSet<DriverRating> DriverRatings { get; set; }
	public DbSet<Cab> Cabs { get; set; }
	public DbSet<Payment> Payments { get; set; }
	public DbSet<TripRequest> TripRequests { get; set; }
	public DbSet<Trip> Trips { get; set; }
	#endregion

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection")); // Or whatever DB provider you're using
		}
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		// TODO:- if postgres is not being used, this method should not get called. Try Open-Closed principle!
		// To make sure that the PostGIS extension is installed in your database:
		// builder.HasPostgresExtension("postgis");

		// restrict all cascade delete
		foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
		{
			relationship.DeleteBehavior = DeleteBehavior.Cascade; // default behavior
		}

		#region Apply Entity Configurations

		// Note:- In a 1-1 reln, we have to decide which should be the parent.
		// and the dependent one's FK will be auto set by EF Core.

		new CabConfiguration().Configure(builder.Entity<Cab>());
		new CustomerConfiguration().Configure(builder.Entity<Customer>());
		new DriverConfiguration().Configure(builder.Entity<Driver>());
		new CustomerRatingConfiguration().Configure(builder.Entity<CustomerRating>());
		new DriverRatingConfiguration().Configure(builder.Entity<DriverRating>());
		new PaymentConfiguration().Configure(builder.Entity<Payment>());
		new TripConfiguration().Configure(builder.Entity<Trip>());
		new TripRequestConfiguration().Configure(builder.Entity<TripRequest>());

		#endregion
	}
}
