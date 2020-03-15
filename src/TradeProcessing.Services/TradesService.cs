using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TradeProcessing.Domain.Entities;
using TradeProcessing.Domain.Publishers;
using TradeProcessing.Domain.Services;

namespace TradeProcessing.Services
{
    public class TradesService : ITradesService
    {
        private readonly ITradeConfirmationsPublisher _tradeConfirmationsPublisher;
        private readonly ILogger<TradesService> _logger;

        public TradesService(
            ITradeConfirmationsPublisher tradeConfirmationsPublisher,
            ILogger<TradesService> logger)
        {
            _tradeConfirmationsPublisher = tradeConfirmationsPublisher;
            _logger = logger;
        }

        public async Task ProcessAsync(Trade trade)
        {
            await _tradeConfirmationsPublisher.ConfirmAsync(trade.Id);

            _logger.LogInformation("Trade confirmed. {@Trade}", trade);
        }
    }
}
