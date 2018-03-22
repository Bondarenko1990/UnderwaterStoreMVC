using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApplication1.Domain.Core
{
    [Table("Arbalets")]
    public class Arbalet : Product
    {
        // Диаметр гарпуна  
        [Display(Name = "Диаметр гарпуна")]
        public decimal DiametrGarpun{ get; set; }

        // Диаметр тяг
        [Display(Name = "Диаметр тяг")]
        public int DiametrTyagi { get; set; }

        // Кол-во тяг
        [Display(Name = "Кол-во тяг")]
        public int SumTyagi { get; set; }

        // Кол-во тяг
        [Display(Name = "Установка доп. тяги")]
        public string DopTyagi { get; set; }

        // Длина ружья
        [Display(Name = "Длина")]
        public int Length { get; set; }
    }
}
