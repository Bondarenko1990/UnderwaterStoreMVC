using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Core
{
    [Table("Nozhis")]
    public class Nozhi : Product
    {
        [Display(Name = "Стропорез")]
        [UIHint("Boolean")]
        public bool Stroporez { get; set; }

        [Display(Name = "Зубчатый край")]
        [UIHint("Boolean")]
        public bool ZubKrai { get; set; }


        [Display(Name = "Материал лезвия")]
        public string Material { get; set; }

        [Display(Name = "Длина лезвия, см")]
        public decimal Length { get; set; }

        public Nozhi()
        {
            this.Stroporez = false;
            this.ZubKrai = false;
            this.Material = "Нет данных";
            this.Length = 0;
        }
    }
}

