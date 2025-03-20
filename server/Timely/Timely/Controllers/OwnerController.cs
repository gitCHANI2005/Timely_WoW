using Autofac.Core;
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
        private readonly IService<Owner> _ownerService;
        private readonly JwtService _jwtService;

        public OwnerController(IRepository<Owner> ownerRepository, JwtService jwtService, IService<Owner> ownerService)
        {
            _ownerRepository = ownerRepository;
            _jwtService = jwtService;
            _ownerService = ownerService;
        }

        [HttpPost]
        public Owner Register([FromBody] OwnerDto owner)
        {
            string role = owner.Role + "";
            if (string.IsNullOrEmpty(owner.Email) || string.IsNullOrEmpty(owner.Password) || string.IsNullOrEmpty(role))
            {
                Console.WriteLine("Email, password, and role are required.");
            }
            Owner o = _ownerService.RegisterOwner(owner);
            return o;
        }

        [HttpGet]
        [Authorize]
        public List<Owner> Get()
        {
            return _ownerRepository.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize]
        public Owner Get(int id)
        {
            return _ownerRepository.Get(id);
        }

        [HttpPut("{id}")]
        [Authorize]
        public Owner Put(int id, [FromBody] Owner updateOwner)
        {
            var existingOwner = _ownerRepository.Get(id);
            _ownerRepository.UpdateItem(id, updateOwner);
            return existingOwner;
        }


        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
            var existingOwner = _ownerRepository.Get(id);
            _ownerRepository.DeleteItem(id);
        }
    }
}
