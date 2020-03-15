using JetBrains.Annotations;

namespace TradeProcessing.Configuration.Service.Rabbit.Publishers
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class RabbitPublishers
    {
        public PublisherSettings TradeConfirmations { get; set; }
    }
}
