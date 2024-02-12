using Core.Domain.Entities;
using Core.Dto;

namespace Core.IServices
{
    public interface IOrdersService
    {
        public Task<List<Order>> GetOrdersAsync();

        public Task<Order?> PostOrderAsync(OrderAddRequest orderAddRequest);

        public Task<bool> DeleteOrderAsync(Guid orderId);
    }
}
