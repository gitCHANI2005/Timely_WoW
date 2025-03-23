using Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer? customer { get; set; }
        public int DeliverId { get; set; }

        public virtual Deliver? deliver { get; set; }
        public int StoreId { get; set; }

        public virtual Store? store { get; set; }
        public DateTime OrderDate { get; set; }
        public double FinalPrice { get; set; }
        public OrderStatus status { get; set; }
    }
}
