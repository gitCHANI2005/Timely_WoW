using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    public class Customer:User
    {
        public int Id { get; set; }
        public int? CityIdHome { get; set; }
        [ForeignKey("CityIdHome")]
        public virtual City? CityHome { get; set; }
        public string AdressHome { get; set; }
        public int? CityIdWork { get; set; }
        [ForeignKey("CityIdWork")]
        public virtual City? CityWork { get; set; }
        public string AdressWork { get; set; }
        public virtual ICollection<Order>? MyOrders { get; set; }
        public string CardNumber { get; set; }
        public DateTime? CardValidity { get; set; }
        public int CardCvv { get; set; }
    }
}
