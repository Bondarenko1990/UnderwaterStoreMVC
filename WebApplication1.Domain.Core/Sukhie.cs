using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Core
{
    [Table("Sukhies")]
    public class Sukhie : Product 
    {
        [Display(Name = "Материал")]
        public string Material { get; set; }

        [Display(Name = "Молния")]
        public string Molniya { get; set; }
    }
}
