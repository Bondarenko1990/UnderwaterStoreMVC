using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Core
{
    [Table("Perchatkis")]
    public class Perchatki : Product
    {
        [Display(Name = "Покрытие внутреннее")]
        public string Pokritie { get; set; }

        [Display(Name = "Манжеты из голого неопрена")]
        [UIHint("Boolean")]
        public bool Mangeti { get; set; }

        [Display(Name = "Двойная обтюрация")]
        [UIHint("Boolean")]
        public bool Obturaciya { get; set; }

        [Display(Name = "Толщина, мм")]
        public decimal Tolshina { get; set; }

        public Perchatki()
        {
            this.Pokritie = "Нет данных";
            this.Mangeti = false;
            this.Obturaciya = false;
            this.Tolshina = 0;
        }
    }
}
