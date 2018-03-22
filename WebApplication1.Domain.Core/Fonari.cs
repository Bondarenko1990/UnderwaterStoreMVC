using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Core
{
    [Table("Fonaris")]
    public class Fonari : Product
    {
        [Display(Name = "Тип лампы")]
        public string TypeLamp { get; set; }

        [Display(Name = "Тип батареи")]
        public string TypeBattery { get; set; }

        [Display(Name = "Световой поток, люмен")]
        public int Svet { get; set; }

        [Display(Name = "Время работы, часов")]
        public int TimeWork { get; set; }

        [Display(Name = "Количество батарей, шт")]
        public int QuantityBattery { get; set; }

        [Display(Name = "Максимальная рабочая глубина, м")]
        public int MaxGlubina { get; set; }

        public Fonari()
        {
            this.Svet = 0;
            this.TimeWork = 0;
            this.QuantityBattery = 0;
            this.MaxGlubina = 0;
            this.TypeBattery = "Нет данных";
            this.TypeLamp = "Нет данных";
        }
    }
}
