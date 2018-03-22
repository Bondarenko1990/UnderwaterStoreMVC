using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Core
{
    [Table("Trubkis")]
    public class Trubki : Product
    {

        [Display(Name = "Нижний клапан")]
        [UIHint("Boolean")]
        public bool Klapan { get; set; }

        [Display(Name = "Материал")]
        public string Material { get; set; }

        [Display(Name = "Гофрированная вставка")]
        [UIHint("Boolean")]
        public bool GofrVstavka { get; set; }

        [Display(Name = "Freetop")]
        [UIHint("Boolean")]
        public bool Freetop { get; set; }

        [Display(Name = "Применение")]
        public string Primenenie { get; set; }

    }
}
