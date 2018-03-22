using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Core
{
    [Table("Noskis")]
    public class Noski : Product
    {
        [Display(Name = "Подошва")]
        public string Podoshva { get; set; }

        [Display(Name = "Покрытие внешнее")]
        public string PokritieVneshnee { get; set; }

        [Display(Name = "Покрытие внутреннее")]
        public string PokritieVnutrenee { get; set; }

        [Display(Name = "Манжеты из голого неопрена")]
        [UIHint("Boolean")]
        public bool Mangeti { get; set; }

        [Display(Name = "Толщина, мм")]
        public decimal Tolshina { get; set; }

        public Noski()
        {
            this.Podoshva = "Нет данных";
            this.PokritieVneshnee = "Нет данных";
            this.PokritieVnutrenee = "Нет данных";
            this.Mangeti = false;
            this.Tolshina = 0;
        }
    }
}
