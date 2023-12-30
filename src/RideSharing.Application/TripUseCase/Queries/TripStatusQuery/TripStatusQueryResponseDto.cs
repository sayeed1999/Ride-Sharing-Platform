﻿using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripUseCase.Queries.TripStatusQuery
{
	public class TripStatusQueryResponseDto
	{
		public TripStatusQueryResponseDto(Trip trip)
		{
			TripId = trip.Id;
			CustomerId = trip.CustomerId;
			DriverId = trip.DriverId;
			Source = trip.Source;
			Destination = trip.Destination;
			TripStatus = trip.TripStatus.ToString();
		}

		public Guid TripId { get; set; }
		public Guid CustomerId { get; set; }
		public Guid DriverId { get; set; }
		public string Source { get; set; }
		public string Destination { get; set; }
		public string TripStatus { get; set; }
	}
}
