using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Dtos;
using Service.Interfaces;
using Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Timely.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IRepository<Owner> _ownerRepository;
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;

        public OwnerController(IRepository<Owner> ownerRepository, JwtService jwtService, IUserService userService)
        {
            _ownerRepository = ownerRepository;
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost("Register")]
        public string Register([FromBody] OwnerDto owner)
        {
            string role = owner.Role + "";

            if (string.IsNullOrEmpty(owner.Email) || string.IsNullOrEmpty(owner.Password) || string.IsNullOrEmpty(role))
            {
                return ("Email, password, and role are required.");
            }

            string token = _userService.RegisterOwner(owner);
            return token;
        }

        // GET: api/<OwnerController>
        [HttpGet]
        [Authorize]
        public List<Owner> Get()
        {
            return _ownerRepository.GetAll();
        }

        // GET api/<OwnerController>/5
        [HttpGet("{id}")]
        [Authorize]
        public Owner Get(int id)
        {
            return _ownerRepository.Get(id);
        }

        // POST api/<OwnerController>
        [HttpPost]
        //[Authorize]
        public ActionResult<Owner> Post([FromBody] Owner newOwner)
        {
            if (newOwner == null)
            {
                return BadRequest("Customer cannot be null");
            }
            try
            {
                var createdOwner = _ownerRepository.AddItem(newOwner);
                return CreatedAtAction(nameof(Get), new { id = createdOwner.Id }, createdOwner);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT api/<OwnerController>/5
        [HttpPut("{id}")]
        [Authorize]
        public Owner Put(int id, [FromBody] Owner updateOwner)
        {
            var existingOwner = _ownerRepository.Get(id);
            _ownerRepository.UpdateItem(id, updateOwner);
            return existingOwner;
        }

        // DELETE api/<OwnerController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
            var existingOwner = _ownerRepository.Get(id);
            _ownerRepository.DeleteItem(id);
        }
    }
}
