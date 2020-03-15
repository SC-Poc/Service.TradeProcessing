using System;
using System.Linq;
using System.Threading.Tasks;
using Lykke.RabbitMqBroker;
using Lykke.RabbitMqBroker.Subscriber;
using MatchingEngine.Client.Contracts.Outgoing;
using Microsoft.Extensions.Logging;
using TradeProcessing.Configuration.Service.Rabbit.Subscribers;
using TradeProcessing.Domain.Entities;
using TradeProcessing.Domain.Services;
using Swisschain.LykkeLog.Adapter;

namespace TradeProcessing.Rabbit.Subscribers
{
    public class EventsSubscriber : IDisposable
    {
        private readonly SubscriberSettings _settings;
        private readonly ITradesService _tradesService;
        private readonly ILogger<EventsSubscriber> _logger;

        private RabbitMqSubscriber<ExecutionEvent> _subscriber;

        public EventsSubscriber(
            SubscriberSettings settings,
            ITradesService tradesService,
            ILogger<EventsSubscriber> logger)
        {
            _settings = settings;
            _tradesService = tradesService;
            _logger = logger;
        }

        public void Start()
        {
            var settings = new RabbitMqSubscriptionSettings
            {
                ConnectionString = _settings.ConnectionString,
                ExchangeName = _settings.Exchange,
                QueueName = $"{_settings.Exchange}.{_settings.QueueSuffix}",
                DeadLetterExchangeName = null,
                IsDurable = false
            };

            _subscriber = new RabbitMqSubscriber<ExecutionEvent>(LegacyLykkeLogFactoryToConsole.Instance,
                    settings,
                    new ResilientErrorHandlingStrategy(LegacyLykkeLogFactoryToConsole.Instance, settings,
                        TimeSpan.FromSeconds(10)))
                .SetMessageDeserializer(new GoogleProtobufMessageDeserializer<ExecutionEvent>())
                .SetMessageReadStrategy(new MessageReadQueueStrategy())
                .Subscribe(ProcessMessageAsync)
                .CreateDefaultBinding()
                .Start();
        }

        public void Stop()
        {
            _subscriber?.Stop();
        }

        public void Dispose()
        {
            _subscriber?.Dispose();
        }

        private async Task ProcessMessageAsync(ExecutionEvent message)
        {
            try
            {
                var trades = message.Orders
                    .SelectMany(order => order.Trades)
                    .Where(trade => trade.Status == MatchingEngine.Client.Contracts.Outgoing.TradeStatus.Pending)
                    .GroupBy(trade => trade.TradeId)
                    .Select(group => group.First())
                    .Select(trade => new Trade
                    {
                        Id = Guid.Parse(trade.TradeId),
                        BaseAssetId = trade.BaseAssetId,
                        QuotingAssetId = trade.QuotingAssetId,
                        Price = decimal.Parse(trade.Price),
                        BaseVolume = decimal.Parse(trade.BaseVolume),
                        QuotingVolume = decimal.Parse(trade.QuotingVolume),
                        Status = Enum.Parse<Domain.Entities.TradeStatus>(trade.Status.ToString()),
                        Timestamp = trade.Timestamp.ToDateTime()
                    })
                    .ToList();

                foreach (var trade in trades)
                    await _tradesService.ProcessAsync(trade);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An error occurred during processing lykke order book. {@Message}",
                    message);
            }
        }
    }
}
