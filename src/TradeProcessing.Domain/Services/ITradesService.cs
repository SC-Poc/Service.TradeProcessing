using System.Threading.Tasks;
using TradeProcessing.Domain.Entities;

namespace TradeProcessing.Domain.Services
{
    public interface ITradesService
    {
        Task ProcessAsync(Trade trade);
    }
}
