using Autofac;
using TradeProcessing.Configuration;
using TradeProcessing.Domain.Publishers;
using TradeProcessing.Managers;
using TradeProcessing.Rabbit.Publishers;
using TradeProcessing.Rabbit.Subscribers;

namespace TradeProcessing
{
    public class AutofacModule : Module
    {
        private readonly AppConfig _config;

        public AutofacModule(AppConfig config)
        {
            _config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StartupManager>()
                .SingleInstance();

            builder.RegisterType<EventsSubscriber>()
                .WithParameter("settings", _config.TradeProcessingService.Rabbit.Subscribers.Events)
                .SingleInstance();

            builder.RegisterType<TradeConfirmationsPublisher>()
                .As<ITradeConfirmationsPublisher>()
                .AsSelf()
                .WithParameter("settings", _config.TradeProcessingService.Rabbit.Publishers.TradeConfirmations)
                .SingleInstance();
        }
    }
}
