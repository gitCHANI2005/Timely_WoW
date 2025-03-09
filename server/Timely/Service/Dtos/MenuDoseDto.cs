using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class MenuDoseDto
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        public double cost { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int countLikes { get; set; }
        public double AvgLikes { get; set; }
        public byte[]? Image { get; set; }//תמונת   
        public IFormFile? File { get; set; }

    }
}
