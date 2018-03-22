using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Core
{
    [Table("Arbaletnaya_tyagas")]
    public class Arbaletnaya_tyaga : Product
    {
        [Display(Name = "Диаметр")]
        public decimal Diametr { get; set; }

        [Display(Name = "Вид тяги")]
        public string Type { get; set; }
    }
}
