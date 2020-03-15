namespace TradeProcessing.Domain.Entities
{
    /// <summary>
    /// Specifies a trade status.
    /// </summary>
    public enum TradeStatus
    {
        /// <summary>
        /// Unspecified status.
        /// </summary>
        None,

        /// <summary>
        /// Pending trade.
        /// </summary>
        Pending,

        /// <summary>
        /// Confirmed trade.
        /// </summary>
        Confirmed,

        /// <summary>
        /// Rejected trade.
        /// </summary>
        Rejected
    }
}
