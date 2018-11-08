using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_terminal
{
    public class CalculatePrice
    {
        // calculates price and quanity of item
        //returns the line 
        //decimal plusTax = (total * taxValue) + total;
        //decimal finalTotal = Math.Round(plusTax, 2);
        public decimal Subtotal { get; set; }
        public decimal Tax = .06m;
        public CalculatePrice()
        {
        }

        public CalculatePrice(decimal subtotal, decimal tax)
        {
            Subtotal = subtotal;
            Tax = tax;
        }
        public decimal GetTax(decimal subtotal)
        {
            return Math.Round(subtotal * Tax, 2);
        }
        

    }
}
