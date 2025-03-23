using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Timely.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrders<Order> _orders;
        public OrderController(IRepository<Order> orderRepository, IOrders<Order> orders)
        {
            _orderRepository = orderRepository;
            _orders = orders;
        }
        [HttpGet]
        public List<Order> Get()
        {
            return _orderRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Order Get(int id)
        {
            return _orderRepository.Get(id);
        }

        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order newOrder)
        {
            if (newOrder == null)
            {
                return BadRequest("Customer cannot be null");
            }
            try
            {
                var createdOrder = _orderRepository.AddItem(newOrder);
                return CreatedAtAction(nameof(Get), new { id = createdOrder.Id }, createdOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "CustomerOnly")]
        public Order Put(int id, [FromBody] Order updateOrder)
        {
            var existingDeliver = _orderRepository.Get(id);
            _orderRepository.UpdateItem(id, updateOrder);
            return existingDeliver;
        }

        [HttpGet("by-deliver/{DeliverId}")]
        //[Authorize(Policy = "DeliverOnly")]
        public List<Order> GetOrdersById(int DeliverId)
        {
            List<Order> orders = _orders.GetOrdersByDeliverId(DeliverId);
            if (orders == null || !orders.Any())
            {
                Console.WriteLine("לא נמצאו הזמנות למשלוחן זה.");
            }
            return orders;
        }


        [HttpDelete("{id}")]
        [Authorize(Policy = "CustomerOnly")]
        public void Delete(int id)
        {
            var existingOrder = _orderRepository.Get(id);
            _orderRepository.DeleteItem(id);
        }
    }
}
