using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class CustomerDto:UserDto
    {
        public int Id { get; set; }
        public string CityHome { get; set; }
        public int CityIdHome { get; set; }
        public string AdressHome { get; set; }
        public string CityWork { get; set; }
        public int CityIdWork { get; set; }
        public string AdressWork { get; set; }
        public string CardNumber { get; set; }
        public DateTime CardValidity { get; set; }
        public int CardCvv { get; set; }
    }
}
