using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Interfaces;
using Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Timely.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IService<StoreDto> _storeService;
        //public static string _directory = Environment.CurrentDirectory + "/images/";

        public StoreController(IService<StoreDto> storeService)
        {
                _storeService = storeService;
        }

        // GET: api/<StoreController>
        [HttpGet]
        public List<StoreDto> Get()
        {
            return _storeService.getAll();
        }

        // GET api/<StoreController>/5
        [HttpGet("{id}")]
        public StoreDto Get(int id)
        {
            return _storeService.getById(id);
        }
        [HttpGet("getImage/{id}")]
        public IActionResult getImage(int id)
        {
            StoreDto s = _storeService.getById(id);
            return File(s.Image, "image/jpeg");
        }

        // POST api/<StoreController>
        [HttpPost]
        public StoreDto Post([FromForm] StoreDto value)
        {
            if (value.File != null)
            {
                var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                // בדוק אם התיקייה קיימת, ואם לא – צור אותה
                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                // צור את הנתיב המלא של הקובץ
                var filePath = Path.Combine(imageDirectory, value.File.FileName);
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    value.File.CopyTo(fs);
                }
                value.Image = System.IO.File.ReadAllBytes(filePath);
            }
            return _storeService.addItem(value);
        }

        // PUT api/<StoreController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StoreDto value)
        {
            if (value == null)
            {
                return BadRequest("Invalid data.");
            }
            var existingStore = _storeService.getById(id);
            if (existingStore == null)
            {
                return NotFound("Store not found.");
            }
            // עדכון פרטי הקטגוריה
            var updatedStore = _storeService.Update(id, value);

            return Ok(updatedStore);  // מחזיר את הקטגוריה המעודכנת
        }

        // DELETE api/<StoreController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingStore = _storeService.getById(id);
            if (existingStore == null)
            {
                return NotFound("Store not found.");
            }

            // מחיקת הקטגוריה
            _storeService.Delete(id);

            return NoContent();  
        }
    }
}
