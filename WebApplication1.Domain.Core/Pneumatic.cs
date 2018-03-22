using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Domain.Core
{
    [Table("Pneumatics")]
    public class Pneumatic : Product
    {

        [Display(Name = "Диаметр гарпуна, мм")]
        [UIHint("Decimal")]
        public decimal Garpun { get; set; }

        [Display(Name = "Регулятор боя")]
        [UIHint("Boolean")]
        public bool RegPower { get; set; }

        [Display(Name = "Длина")]
        public int Length { get; set; }

    }
}
