using Autofac;
using TradeProcessing.Domain.Services;

namespace TradeProcessing.Services
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TradesService>()
                .As<ITradesService>()
                .SingleInstance();
        }
    }
}
