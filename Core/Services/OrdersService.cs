using Core.Domain.Entities;
using Core.Domain.IRepositories;
using Core.Dto;
using Core.IServices;
using Microsoft.Extensions.Logging;

namespace Core.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ILogger<OrdersService> _logger;
        private readonly IOrdersRepository _ordersRepository;

        public OrdersService(ILogger<OrdersService> logger, IOrdersRepository ordersRepository)
        {
            _logger = logger;
            _ordersRepository = ordersRepository;
        }


        #region GET

        public async Task<List<Order>> GetOrdersAsync()
        {
            _logger.LogInformation("GetOrdersAsync method in OrdersService");

            List<Order> orders = await _ordersRepository.GetOrdersAsync();

            return orders;
        }

        #endregion

        #region POST

        public async Task<Order?> PostOrderAsync(OrderAddRequest orderAddRequest)
        {
            _logger.LogInformation("PostOrderAsync method in OrdersService");

            Order order = new Order()
            {
                FirstName = orderAddRequest.FirstName,
                LastName = orderAddRequest.LastName,
                Email = orderAddRequest.Email,
                Comment = orderAddRequest.Comment,
                PhoneNumber = orderAddRequest.PhoneNumber,
                Insurance = orderAddRequest.Insurance,
                StartOrder = orderAddRequest.StartOrder,
                EndOrder = orderAddRequest.EndOrder,
                CarId = orderAddRequest.CarId
            };

            await _ordersRepository.PostOrderAsync(order);

            if(! await _ordersRepository.IsSavedAsync())
            {
                _logger.LogError("Error occured while storing the order in the database.");
                return null;
            }

            return order;
        }

        #endregion

        #region DELETE

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            _logger.LogInformation("DeleteOrderAsync method in OrdersService");

            Order? order = await _ordersRepository.GetOrderByIdAsync(orderId);

            if (order == null)
            {
                _logger.LogError($"Order was not found, ID: {orderId}");
                return false;
            }

            _ordersRepository.DeleteOrder(order);

            if (! await _ordersRepository.IsSavedAsync())
            {
                _logger.LogError("Error occurred while deleting the order from the database.");
                return false;
            }

            return true;
        }

        #endregion
    }
}
