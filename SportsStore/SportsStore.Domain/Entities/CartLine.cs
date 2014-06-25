using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsStore.Domain.Entities
{
    public class CartLine
    {
        public int Quantity { get; set; }

        public Product Product { get; set; }
    }
}
