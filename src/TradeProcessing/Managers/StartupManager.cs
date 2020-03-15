using System.Threading.Tasks;
using TradeProcessing.Rabbit.Publishers;
using TradeProcessing.Rabbit.Subscribers;

namespace TradeProcessing.Managers
{
    public class StartupManager
    {
        private readonly TradeConfirmationsPublisher _tradeConfirmationsPublisher;
        private readonly EventsSubscriber _eventsSubscriber;

        public StartupManager(
            TradeConfirmationsPublisher tradeConfirmationsPublisher,
            EventsSubscriber eventsSubscriber)
        {
            _tradeConfirmationsPublisher = tradeConfirmationsPublisher;
            _eventsSubscriber = eventsSubscriber;
        }

        public Task StartAsync()
        {
            _tradeConfirmationsPublisher.Start();
            _eventsSubscriber.Start();

            return Task.CompletedTask;
        }
    }
}
