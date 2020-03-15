using System;
using System.Threading.Tasks;

namespace TradeProcessing.Domain.Publishers
{
    public interface ITradeConfirmationsPublisher
    {
        Task ConfirmAsync(Guid tradeId);

        Task RejectAsync(Guid tradeId, string reason);
    }
}
