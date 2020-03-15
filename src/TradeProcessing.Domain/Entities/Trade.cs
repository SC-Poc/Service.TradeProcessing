using System;

namespace TradeProcessing.Domain.Entities
{
    /// <summary>
    /// Represents a trade.
    /// </summary>
    public class Trade
    {
        /// <summary>
        /// The trade identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The base asset identifier.
        /// </summary>
        public string BaseAssetId { get; set; }

        /// <summary>
        /// The quoting asset identifier.
        /// </summary>
        public string QuotingAssetId { get; set; }

        /// <summary>
        /// The trade price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The trade base volume.
        /// </summary>
        public decimal BaseVolume { get; set; }

        /// <summary>
        /// The trade quoting volume.
        /// </summary>
        public decimal QuotingVolume { get; set; }

        /// <summary>
        /// The trade status.
        /// </summary>
        public TradeStatus Status { get; set; }

        /// <summary>
        /// The date and time of creation.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
