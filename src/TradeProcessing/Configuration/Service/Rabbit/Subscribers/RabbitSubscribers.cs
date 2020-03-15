using JetBrains.Annotations;

namespace TradeProcessing.Configuration.Service.Rabbit.Subscribers
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class RabbitSubscribers
    {
        public SubscriberSettings Events { get; set; }
    }
}
