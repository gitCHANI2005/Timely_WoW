using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Repository.Interfaces;
using Service.Dtos;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Timely.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverController : ControllerBase

    {
        private readonly IRepository<Deliver> _deliverrepository;
        private readonly IUserService _userService;

        public DeliverController(IRepository<Deliver> deliverrepository, IUserService userService)
        {
            _deliverrepository = deliverrepository;
            _userService = userService;
        }

        [HttpPost("Register")]
        public string Register([FromBody] DeliverDto deliver)
        {
            string role = deliver.Role + "";

            if (string.IsNullOrEmpty(deliver.Email) || string.IsNullOrEmpty(deliver.Password) || string.IsNullOrEmpty(role))
            {
                return ("Email, password, and role are required.");
            }

            string token = _userService.RegisterDeliver(deliver);
            return token;
        }
        // GET: api/<DeliverController>
        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public List<Deliver> Get()
        {
            return _deliverrepository.GetAll();
        }

        // GET api/<DeliverController>/5
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public Deliver Get(int id)
        {
            return _deliverrepository.Get(id);
        }

        // POST api/<DeliverController>
        [HttpPost]
        public ActionResult<Deliver> Post([FromBody] Deliver newDeliver)
        {
            if (newDeliver == null)
            {
                return BadRequest("Customer cannot be null");
            }
            try
            {
                var createdDeliver = _deliverrepository.AddItem(newDeliver);
                return CreatedAtAction(nameof(Get), new { id = createdDeliver.Id }, createdDeliver);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT api/<DeliverController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "DeliverOnly")]
        public Deliver Put(int id, [FromBody] Deliver updateDeliver)
        {
            var existingDeliver = _deliverrepository.Get(id);
            _deliverrepository.UpdateItem(id, updateDeliver);

            return existingDeliver;
        }

        // DELETE api/<DeliverController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "DeliverOnly")]
        public void Delete(int id)
        {
            var existingDeliver = _deliverrepository.Get(id);
            _deliverrepository.DeleteItem(id);
        }
    }
}
