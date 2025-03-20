using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class DeliverDto:UserDto
    {
        public int Id { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public int CityId { get; set; }
        public string cityName { get; set; }
        public string NumOfCar { get; set; }
        public string BankNumber { get; set; }
        public string BankAccount { get; set; }
        public string BankBranch { get; set; }
    }
}
