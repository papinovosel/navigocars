using Core.Domain.Entities;
using Core.Domain.IRepositories;
using Core.Dto;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ApplicationDbContext _db;

        public OrdersRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        //GET

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _db.Orders.ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return await _db.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }

        //POST

        public async Task PostOrderAsync(Order order)
        {
            await _db.Orders.AddAsync(order);
        }


        //DELETE

        public void DeleteOrder(Order order)
        {
            _db.Orders.Remove(order);
        }


        //VALIDATION

        public async Task<bool> IsSavedAsync()
        {
            int saved = await _db.SaveChangesAsync();

            return saved > 0;
        }
    }
}
