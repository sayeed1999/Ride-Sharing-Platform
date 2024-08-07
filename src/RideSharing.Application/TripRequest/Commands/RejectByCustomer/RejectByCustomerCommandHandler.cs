﻿using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Processor.TransitionChecker;

namespace RideSharing.Application.TripRequest.Commands.RejectByCustomer
{
	public class RejectByCustomerCommandHandler(
		IUnitOfWork unitOfWork,
		ITripRequestEventMessageBus messageBus,
		IRideProcessingService rideProcessingService)
		: IRequestHandler<RejectByCustomerCommandDto, Result<long>>
	{
		public async Task<Result<long>> Handle(RejectByCustomerCommandDto request, CancellationToken cancellationToken)
		{
			// Step 1: check customer exists
			var customerInDB = await unitOfWork.CustomerRepository.FindByIdAsync(request.CustomerId);

			if (customerInDB == null)
			{
				return Result.Failure<long>("Customer is not found.");
			}

			// Step 2: check trip request exists
			var activeTripRequest = await unitOfWork.TripRequestRepository.GetActiveTripRequestForCustomer(request.CustomerId);

			if (activeTripRequest == null)
			{
				return Result.Failure<long>("Customer has no active trip request.");
			}

			// ** Security check !
			if (activeTripRequest.Id != request.TripRequestId)
			{
				return Result.Failure<long>("Active trip request for customer does not match !!");
			}

			// Step 3: Check transition valid or not
			var transitionValid = await rideProcessingService.IsTripRequestTransitionValid(activeTripRequest.Status, TripRequestStatus.CUSTOMER_REJECTED_DRIVER);

			if (!transitionValid)
			{
				return Result.Failure<long>("TripRequest Status cannot be changed to desired status.");
			}

			// Step 4: prepare entity
			activeTripRequest.Modify(TripRequestStatus.CUSTOMER_REJECTED_DRIVER);

			// Step 5: perform database operations
			try
			{
				// Note: log table is inserted from database triggers, not api

				unitOfWork.TripRequestRepository.Update(activeTripRequest);

				// call UoW to save the changes in db.
				var result = await unitOfWork.SaveChangesAsync();

				if (result.IsFailure)
				{
					return Result.Failure<long>(result.Error);
				}

				messageBus.PublishAsync(activeTripRequest.GetTripRequestDto());

				// Last Step: return result

				return Result.Success(request.TripRequestId);
			}
			catch (Exception ex)
			{
				return Result.Failure<long>($"Failed with error: {ex.Message}");
			}
		}
	}
}
