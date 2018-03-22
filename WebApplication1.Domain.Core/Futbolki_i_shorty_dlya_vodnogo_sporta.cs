using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Core
{
    [Table("Futbolki_i_shorty_dlya_vodnogo_sportas")]
    public class Futbolki_i_shorty_dlya_vodnogo_sporta : Product
    {
        
        [Display(Name = "Тип")]
        public string Type { get; set; }

        [Display(Name = "Пол")]
        public string Pol { get; set; }
    }
}
