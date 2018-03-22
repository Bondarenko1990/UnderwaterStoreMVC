using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Core
{
    [Table("Gruza_i_gruzovye_sistemys")]
    public class Gruza_i_gruzovye_sistemy : Product
    {
        [Display(Name = "Тип")]
        public string Type { get; set; }

        [Display(Name = "Вместимость/вес (кг)")]
        public int Kg { get; set; }

        public Gruza_i_gruzovye_sistemy()
        {
            this.Type = "Нет данных";
            this.Kg = 0;
        }
    }
}
