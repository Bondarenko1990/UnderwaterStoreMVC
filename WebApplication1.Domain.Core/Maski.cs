using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Core
{
    [Table("Maskis")]
    public class Maski : Product
    {
        [Display(Name = "Применение")]
        public string Primenenie { get; set; }

        [Display(Name = "Кол-во стекол")]
        public string Glass { get; set; }
    }
}
