using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    public class Deliver:User
    {
        public int Id { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City city { get; set; }
        public string NumOfCar { get; set; }
        public string BankNumber { get; set; }
        public string BankAccount { get; set; }
        public string BankBranch { get; set; }
    }
}
