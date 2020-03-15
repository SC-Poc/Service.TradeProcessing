using JetBrains.Annotations;
using TradeProcessing.Configuration.Service.Rabbit.Publishers;
using TradeProcessing.Configuration.Service.Rabbit.Subscribers;

namespace TradeProcessing.Configuration.Service.Rabbit
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class RabbitSettings
    {
        public RabbitSubscribers Subscribers { get; set; }

        public RabbitPublishers Publishers { get; set; }
    }
}
