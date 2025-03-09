using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Repository.Interfaces;
using Repository.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Timely.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepository;
        public OrderController(IRepository<Order> orderRepository)
        {
            _orderRepository=orderRepository;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public List<Order> Get()
        {
            return _orderRepository.GetAll();
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public Order Get(int id)
        {
            return _orderRepository.Get(id);
        }

        // POST api/<OrderController>
        [HttpPost]
        [Authorize(Policy = "CustomerOnly")]

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

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "CustomerOnly")]
        public Order Put(int id, [FromBody] Order updateOrder)
        {
            var existingDeliver = _orderRepository.Get(id);
            _orderRepository.UpdateItem(id, updateOrder);
            return existingDeliver;
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "CustomerOnly")]
        public void Delete(int id)
        {
            var existingOrder = _orderRepository.Get(id);
            _orderRepository.DeleteItem(id);
        }
    }
}
