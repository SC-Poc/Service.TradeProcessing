using System;
using System.IO;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Lykke.RabbitMqBroker.Publisher;
using Lykke.RabbitMqBroker.Subscriber;
using MatchingEngine.Client.Contracts.Outgoing;
using Microsoft.Extensions.Logging;
using Swisschain.LykkeLog.Adapter;
using TradeProcessing.Configuration.Service.Rabbit.Publishers;
using TradeProcessing.Domain.Publishers;

namespace TradeProcessing.Rabbit.Publishers
{
    public class TradeConfirmationsPublisher : ITradeConfirmationsPublisher, IDisposable
    {
        private readonly PublisherSettings _settings;
        private readonly ILogger<TradeConfirmationsPublisher> _logger;
        private RabbitMqPublisher<TradeResponse> _publisher;

        public TradeConfirmationsPublisher(
            PublisherSettings settings,
            ILogger<TradeConfirmationsPublisher> logger)
        {
            _settings = settings;
            _logger = logger;
        }

        public void Start()
        {
            var settings = new RabbitMqSubscriptionSettings
            {
                ConnectionString = _settings.ConnectionString, ExchangeName = _settings.Exchange
            };

            _publisher = new RabbitMqPublisher<TradeResponse>(LegacyLykkeLogFactoryToConsole.Instance, settings)
                .SetSerializer(new GoogleProtobufMessageSerializer<TradeResponse>())
                .DisableInMemoryQueuePersistence()
                .Start();
        }

        public void Stop()
        {
            _publisher?.Stop();
        }

        public Task ConfirmAsync(Guid tradeId)
        {
            return _publisher.ProduceAsync(new TradeResponse
            {
                TraderId = tradeId.ToString(),
                Status = TradeStatus.Confirmed,
                StatusReason = string.Empty,
                Timestamp = Timestamp.FromDateTime(DateTime.UtcNow)
            });
        }

        public Task RejectAsync(Guid tradeId, string reason)
        {
            return _publisher.ProduceAsync(new TradeResponse
            {
                TraderId = tradeId.ToString(),
                Status = TradeStatus.Rejected,
                StatusReason = reason,
                Timestamp = Timestamp.FromDateTime(DateTime.UtcNow)
            });
        }

        public void Dispose()
        {
            _publisher?.Stop();
        }
    }
}
