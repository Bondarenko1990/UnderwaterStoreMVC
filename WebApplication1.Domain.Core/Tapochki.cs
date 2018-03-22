using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Core
{
    [Table("Tapochkis")]
    public class Tapochki : Product
    {
        [Display(Name = "Подошва")]
        public string Podoshva { get; set; }

        [Display(Name = "Материал подошвы")]
        public string Material { get; set; }

        [Display(Name = "Возраст")]
        public string Age { get; set; }

        public Tapochki()
        {
            this.Podoshva = "Нет данных";
            this.Material = "Нет данных";
            this.Age = "Нет данных";
        }
    }
}
