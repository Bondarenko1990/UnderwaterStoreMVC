using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Pneumatic
    {
        // ID пнематического ружья
        public int Id { get; set; }
        // название 
        public string Name { get; set; }        
        // Диаметр гарпуна  
        public int Garpun { get; set; }
        // Регулятор боя
        public bool RegPower { get; set; }
        // Длина ружья
        public int Lehgth { get; set; }
        public string ImageName { get; set; }
        public int Price { get; set; }
        // Производитель бренда
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}