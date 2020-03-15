using JetBrains.Annotations;

namespace TradeProcessing.Configuration.Service.Rabbit.Publishers
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class PublisherSettings
    {
        public string ConnectionString { get; set; }

        public string Exchange { get; set; }
    }
}
