using CSharpFunctionalExtensions;
using MediatR;
using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Application.Trips.Commands.InitiatePayment;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.Application.Trips.Commands.EndTrip;

public class InitiatePaymentHandler(
	IUnitOfWork unitOfWork,
	ITripEventMessageBus tripMessageBus,
	IRideProcessingService rideProcessingService
)
	: IRequestHandler<InitiatePaymentCommand, Result<string>>
{
	public async Task<Result<string>> Handle(InitiatePaymentCommand model, CancellationToken cancellationToken)
	{
		var tripInDB = await unitOfWork.TripRepository.GetTripForCustomerWithPendingPayment(
			model.TripId,
			model.CustomerId);

		if (tripInDB == null)
		{
			return Result.Failure<string>("Ongoing Trip is not found.");
		}

		// TODO: - call service for payment processing and return error if payment failed!

		bool transitionValid = await rideProcessingService.IsTripTransitionValid(tripInDB.TripStatus, TripStatus.PAYMENT_COMPLETED);

		if (!transitionValid)
		{
			return Result.Failure<string>("Trip Status cannot be changed to desired status.");
		}

		tripInDB.Modify(TripStatus.PAYMENT_COMPLETED);

		try
		{
			unitOfWork.TripRepository.Update(tripInDB);

			var result = await unitOfWork.SaveChangesAsync();

			if (result.IsFailure)
			{
				return Result.Failure<string>(result.Error);
			}

			tripMessageBus.PublishAsync(tripInDB.GetTripDto());

			return Result.Success(model.TripId);
		}
		catch (Exception ex)
		{
			return Result.Failure<string>($"Failed with error: {ex.Message}");
		}
	}
}
