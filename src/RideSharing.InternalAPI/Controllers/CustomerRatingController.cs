﻿using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.InternalAPI.Controllers
{
	public class CustomerRatingController : BaseController<CustomerRatingEntity>
	{
		public CustomerRatingController(ICustomerRatingRepository repository) : base(repository)
		{

		}
	}
}
