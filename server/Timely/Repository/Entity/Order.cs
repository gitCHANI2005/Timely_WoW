using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    public enum OrderStatus
    {
        invited,
        preparing,
        ready,
        taken,
        arrived
    }
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }
        public int DeliverId { get; set; }
        [ForeignKey("DeliverId")]
        public virtual Deliver? deliver { get; set; }
        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public virtual Store? store { get; set; }
        public DateTime OrderDate { get; set; }
        public double FinalPrice { get; set; }
        public OrderStatus status { get; set; }
    }
}
