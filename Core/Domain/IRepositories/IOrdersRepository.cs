using Core.Domain.Entities;
using Core.Dto;

namespace Core.Domain.IRepositories
{
    public interface IOrdersRepository
    {
        public Task<List<Order>> GetOrdersAsync();
        public Task<Order?> GetOrderByIdAsync(Guid orderId);

        public Task PostOrderAsync(Order order);

        public void DeleteOrder(Order order);

        public Task<bool> IsSavedAsync();
    }
}
