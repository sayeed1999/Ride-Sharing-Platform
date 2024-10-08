﻿using CSharpFunctionalExtensions;
using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.Persistence.UnitOfWork
{
	public partial class UnitOfWork : IUnitOfWork
	{
		private ApplicationDbContext dbContext { get; init; }

		public UnitOfWork(
			ApplicationDbContext dbContext,
			IBaseRepository<Cab> cabRepository,
			IBaseRepository<CustomerRating> customerRatingRepository,
			IBaseRepository<Customer> customerRepository,
			IBaseRepository<DriverRating> driverRatingRepository,
			IBaseRepository<Driver> driverRepository,
			IBaseRepository<Payment> paymentRepository,
			ITripRepository tripRepository,
			ITripRequestRepository tripRequestRepository,
			IUserContext userContext
			)
		{
			this.dbContext = dbContext;

			CabRepository = cabRepository;
			CustomerRatingRepository = customerRatingRepository;
			CustomerRepository = customerRepository;
			DriverRatingRepository = driverRatingRepository;
			DriverRepository = driverRepository;
			PaymentRepository = paymentRepository;
			TripRepository = tripRepository;
			TripRequestRepository = tripRequestRepository;

			UserContext = userContext;
		}

		public IBaseRepository<Cab> CabRepository { get; }
		public IBaseRepository<CustomerRating> CustomerRatingRepository { get; }
		public IBaseRepository<Customer> CustomerRepository { get; }
		public IBaseRepository<DriverRating> DriverRatingRepository { get; }
		public IBaseRepository<Driver> DriverRepository { get; }
		public IBaseRepository<Payment> PaymentRepository { get; }
		public ITripRepository TripRepository { get; }
		public ITripRequestRepository TripRequestRepository { get; }
		public IUserContext UserContext { get; }

		public async Task<Result<int>> SaveChangesAsync()
		{
			try
			{
				return await dbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return Result.Failure<int>($"Failed with error: {ex.Message}");
			}
		}
	}
}
