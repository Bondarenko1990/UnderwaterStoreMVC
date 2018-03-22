using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Core
{
    [Table("Nakonechniks")]
    public class Nakonechnik : Product
    {

        [Display(Name = "Диаметр резьбы")]
        public decimal DiametrRezbi { get; set; }

        [Display(Name = "Тип")]
        public string Type { get; set; }

    }
}
