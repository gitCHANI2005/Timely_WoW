﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    public class Owner:User
    {
        public int Id { get; set; }
        public virtual ICollection<Store>? stores { get; set; }

    }
}
