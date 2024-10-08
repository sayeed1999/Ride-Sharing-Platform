﻿using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.Application.Abstractions
{
	public interface ITripRepository : IBaseRepository<Trip>
	{
		Task<Trip?> GetActiveTripForCustomer(string customerId);
		Task<Trip?> GetActiveTripForDriver(string driverId);
		Task<Trip?> GetTripForCustomerWithPendingPayment(string tripId, string customerId);
	}
}
