using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Repository.Interfaces;
using Service.Dtos;
using Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Timely.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IRepository<City> _cityrepository;

        public CityController(IRepository<City> cityrepository)
        {
            _cityrepository = cityrepository;
        }
        [HttpGet]
        //[Authorize(Policy = "AdminOnly")]
        public List<City> Get()
        {
            return _cityrepository.GetAll();
        }
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public City Get(int id)
        {
            return _cityrepository.Get(id);
        }
        // POST api/city
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Post([FromBody] City newCity)
        {
            try
            {
                if (newCity == null)
                {
                    return BadRequest("Invalid data.");
                }

                var createdCity = _cityrepository.AddItem(newCity); // יצירת עיר חדשה בעזרת ה-Repository
                return CreatedAtAction(nameof(Get), new { id = createdCity.Id }, createdCity); // מחזיר את העיר שנוצרה
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Intlernal server error: " + ex.Message);
            }
        }
        // PUT api/city/5
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public City Put(int id, [FromBody] City updatedCity)
        {
            //if (updatedCity == null)
            //{
            //    return 
            //        BadRequest("Invalid data.");
            //}

            var existingCity = _cityrepository.Get(id);
            //if (existingCity == null)
            //{
            //    return NotFound("City not found.");
            //}

            // עדכון העיר
            _cityrepository.UpdateItem(id, updatedCity);

            return existingCity; // מחזיר תשובה ללא תוכן (העיר עודכנה)
        }
        // DELETE api/city/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public void Delete(int id)
        {
            var existingCity = _cityrepository.Get(id);
            //if (existingCity == null)
            //{
            //     NotFound("City not found.");
            //}

            // מחיקת העיר
            _cityrepository.DeleteItem(id);

             //NoContent(); // מחזיר תשובה ללא תוכן (העיר נמחקה)
        }

    }
}
