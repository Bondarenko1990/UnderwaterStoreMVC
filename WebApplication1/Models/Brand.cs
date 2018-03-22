using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Brand
    {   
            // ID бренда
            public int Id { get; set; }
            // название бренда
            public string Name { get; set; }
            // Производитель бренда
            public string Manufacturer { get; set; } 

            public ICollection<Pneumatic> Pneumatics { get; set; }
            public Brand()
            {
                   Pneumatics = new List<Pneumatic>();
            }
    }
}