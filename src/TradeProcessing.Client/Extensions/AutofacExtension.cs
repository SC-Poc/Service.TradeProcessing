using System;
using System.Diagnostics.CodeAnalysis;
using Autofac;

namespace TradeProcessing.Client.Extensions
{
    /// <summary>
    /// Extension for client registration.
    /// </summary>
    public static class AutofacExtension
    {
        /// <summary>
        /// Registers <see cref="ITradeProcessingClient"/> in Autofac container using <see cref="TradeProcessingClientSettings"/>.
        /// </summary>
        /// <param name="builder">Autofac container builder.</param>
        /// <param name="settings">The client settings.</param>
        public static void RegisterTradeProcessingClient(
            [NotNull] this ContainerBuilder builder,
            [NotNull] TradeProcessingClientSettings settings)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            builder.RegisterInstance(new TradeProcessingClient(settings))
                .As<ITradeProcessingClient>()
                .SingleInstance();
        }
    }
}
