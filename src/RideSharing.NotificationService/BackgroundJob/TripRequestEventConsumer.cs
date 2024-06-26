﻿using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Common.MessageQueues.Messages;
using RideSharing.NotificationService.Notifier;
using System.Text.Json;

namespace RideSharing.NotificationService.BackgroundJob;

public class TripRequestEventConsumer(
	ITripRequestEventMessageBus messageBus,
	INotifier notifier)
	: BaseEventConsumer(messageBus, notifier)
{
	Func<TripRequestDto, Task> notificationHandler = async (TripRequestDto entity) =>
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
