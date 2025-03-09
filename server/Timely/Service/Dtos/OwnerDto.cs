using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class OwnerDto:UserDto
    {
        public virtual ICollection<StoreDto>? stores { get; set; }
    }
}
