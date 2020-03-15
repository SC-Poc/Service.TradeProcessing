using System;

namespace TradeProcessing.Client
{
    /// <inheritdoc />
    public class TradeProcessingClient : ITradeProcessingClient
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TradeProcessingClient"/>.
        /// </summary>
        /// <param name="settings">The client settings.</param>
        public TradeProcessingClient(TradeProcessingClientSettings settings)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
        }
    }
}
