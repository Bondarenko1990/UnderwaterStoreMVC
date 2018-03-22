using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Core
{
    [Table("Lasty_dlya_okhotys")]
    public class Lasty_dlya_okhoty : Product
    {
        [Display(Name = "Пятка")]
        public string Pyatka { get; set; }

        [Display(Name = "Лопасть")]
        public string Lopast { get; set; }

        [Display(Name = "Материал лопасти")]
        public string Material { get; set; }

        [Display(Name = "Длина лопасти, см")]
        public int Length { get; set; }
    }
}
