using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Domain.Core;

namespace WebApplication1.Services.Interfaces
{
    interface IOrder
    {
        void MakeOrder(Product1 product);
    }
}
