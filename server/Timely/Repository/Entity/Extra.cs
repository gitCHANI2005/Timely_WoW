﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    public class Extra
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double cost { get; set; }
    }
}
