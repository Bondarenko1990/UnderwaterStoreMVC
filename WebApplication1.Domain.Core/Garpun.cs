using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Core
{
    [Table("Garpuns")]
    public class Garpun : Product
    {
        // Диаметр гарпуна  
        [Display(Name = "Диаметр гарпуна")]
        public decimal DiametrGarpun { get; set; }

        // Диаметр резьбы
        [Display(Name = "Диаметр резьбы")]
        public decimal DiametrRezbi { get; set; }

        // Тип
        [Display(Name = "Тип")]
        public string Type { get; set; }

        // Тип ружья
        [Display(Name = "Тип ружья")]
        public string TypeOfGun { get; set; }

        // Длина гарпуна
        [Display(Name = "Длина")]
        public int Length { get; set; }
    }
}
