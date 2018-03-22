using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Core
{
    [Table("Shtany_i_kurtkis")]
    public class Shtany_i_kurtki : Product 
    {
        
        [Display(Name = "Защита на коленях")]
        [UIHint("Boolean")]
        public bool ZashitaKoleni { get; set; }

        [Display(Name = "Защита на локтях")]
        [UIHint("Boolean")]
        public bool ZashitaLokti { get; set; }

        [Display(Name = "Покрытие внешнее")]
        public string PokritieVneshnee { get; set; }

        [Display(Name = "Покрытие внутреннее")]
        public string PokritieVnutrenee { get; set; }

        [Display(Name = "Waterstop на рукавах")]
        [UIHint("Boolean")]
        public bool WaterstopRukava { get; set; }

        [Display(Name = "Waterstop на штанах")]
        [UIHint("Boolean")]
        public bool WaterstopShtani { get; set; }

        [Display(Name = "Тип")]
        public string Type { get; set; }

        [Display(Name = "Толщина, мм")]
        [UIHint("Decimal")]
        [DisplayFormat(DataFormatString = "{0:F1}", ApplyFormatInEditMode = true)]
        public decimal Tolshina { get; set; }

    }
}
