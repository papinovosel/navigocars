using Api.Filters.ActionFilters;
using Core.Domain.Entities;
using Core.Dto;
using Core.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/orders/")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrdersService _ordersService;

        public OrdersController(ILogger<OrdersController> logger, IOrdersService ordersService)
        {
            _logger = logger;
            _ordersService = ordersService;
        }

        [HttpGet("admin")]
        public async Task<IActionResult> GetOrders()
        {
            _logger.LogInformation("GetOrders action method in OrdersController");

            List<Order> orders = await _ordersService.GetOrdersAsync();

            //

            await HttpContext.SignInAsync(User, new AuthenticationProperties()
            {
                IsPersistent = false
            });

            //

            return Ok(orders);
        }


        //POST: api/orders/post

        [HttpPost("post")]
        [TypeFilter(typeof(CheckModelStateFilter))]
        public async Task<IActionResult> AddOrder(OrderAddRequest orderAddRequest)
        {
            _logger.LogInformation("AddOrder action method in OrdersController");

            Order? order = await _ordersService.PostOrderAsync(orderAddRequest);

            if(order == null)
            {
                _logger.LogError("Something went wrong while storing the order into the database");
                return Problem("Something went wrong while storing the order into the database", statusCode: 500, title: "Adding the order");
            }

            return Ok(order);
        }


        //DELETE: api/orders/admin/delete/6FE3DB68-B315-46CA-B0AF-08DC1A688A3F

        [HttpDelete("admin/delete/{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            _logger.LogInformation("DeleteOrder action method in OrdersController");

            if(! await _ordersService.DeleteOrderAsync(orderId))
            {
                return Problem("Delete failed.", statusCode: 404, title: "Delete order");
            }

            return Ok("Delete successful");
        }
    }
}
