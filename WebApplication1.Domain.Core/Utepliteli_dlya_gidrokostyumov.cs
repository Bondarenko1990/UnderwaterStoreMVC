using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Core
{
    [Table("Utepliteli_dlya_gidrokostyumovs")]
    public class Utepliteli_dlya_gidrokostyumov : Product
    {
        [Display(Name = "Толщина, мм")]
        [UIHint("Decimal")]
        [DisplayFormat(DataFormatString = "{0:F1}", ApplyFormatInEditMode = true)]
        public decimal Tolshina { get; set; }
    }
}
