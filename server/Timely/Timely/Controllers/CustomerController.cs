using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using Repository.Entity;
using Repository.Interfaces;
using Service.Dtos;
using Service.Interfaces;
using Service.Services;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Timely.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository<Customer> _customerrepository;
        private readonly JwtService _jwtService;
        private readonly IUserService _userService;

        public CustomerController(IRepository<Customer> customerrepository, JwtService jwtService, IUserService userService)
        {
            _customerrepository = customerrepository;
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost("Register")]
        public string Register([FromBody] CustomerDto customer)
        {
            string role = customer.Role + "";

            if (string.IsNullOrEmpty(customer.Email) || string.IsNullOrEmpty(customer.Password) || string.IsNullOrEmpty(role))
            {
                return ("Email, password, and role are required.");
            }

            string token = _userService.RegisterCustomer(customer);
            return token;
        }

        //[Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public List<Customer> Get()
        {
            return _customerrepository.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public Customer Get(int id)
        {
            return _customerrepository.Get(id);
        }

        // POST api/<CustomerController>
        [HttpPost]
        [Authorize]
        public ActionResult<Customer> Post([FromBody] Customer newCustomer)
        {
            if (newCustomer == null)
            {
                return BadRequest("Customer cannot be null");
            }
            try
            {
                var createdCustomer = _customerrepository.AddItem(newCustomer);
                return CreatedAtAction(nameof(Get), new { id = createdCustomer.Id }, createdCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        [Authorize]
        public Customer Put(int id, [FromBody] Customer updateCustomer)
        {
            var existingCustomer = _customerrepository.Get(id);
            _customerrepository.UpdateItem(id, updateCustomer);

            return existingCustomer;
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}") ]
        [Authorize(Policy = "AdminOnly")]
        public void Delete(int id)
        {
            var existingCustomer = _customerrepository.Get(id);
            _customerrepository.DeleteItem(id);
        }
    }
}
