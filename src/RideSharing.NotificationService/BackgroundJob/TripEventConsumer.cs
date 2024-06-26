﻿using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Messages;
using RideSharing.NotificationService.Notifier;
using System.Text.Json;

namespace RideSharing.NotificationService.BackgroundJob;

public class TripEventConsumer(
	ITripEventMessageBus messageBus,
	INotifier notifier)
	: BaseEventConsumer(messageBus, notifier)
{
	Func<TripDto, Task> notificationHandler = async (TripDto entity) =>
	{
		await Task.Delay(100);
		var message = $"[x] Received Message: {JsonSerializer.Serialize(entity)}";
		await notifier.Notify(message);
	};

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		await messageBus.ConsumeAsync(notificationHandler);
	}
}
