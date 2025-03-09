using Microsoft.AspNetCore.Http;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class StoreDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        //public int IdOwner { get; set; }
        //[ForeignKey("IdOwner")]
        public virtual Owner? owner { get; set; }
        //public int IdCity { get; set; }
        //[ForeignKey("IdCity")]
        public virtual City? city { get; set; }
        public string Address { get; set; }
        //public bool IsManager { get; set; }
        //public bool IsOwner { get; set; }
        public byte[]? Image { get; set; }//תמונת  
        //public ICollection<Extra>? ListExtra { get; set; }
        public TimeSpan? WeekOpen { get; set; }
        public TimeSpan? WeekClose { get; set; }
        public TimeSpan? FridayOpen { get; set; }
        public TimeSpan? FridayClose { get; set; }
        public TimeSpan? ShabbatOpen { get; set; }
        public TimeSpan  ShabbatClose { get; set; }
        public IFormFile? File { get; set; }
    }
}
