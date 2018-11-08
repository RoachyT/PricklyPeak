using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_terminal
{
    public class CalculateTotal: CalculatePrice
    {
        //calculates subtotal, sales tax and grand total
        public decimal FinalTotal {get; set;}
        public CalculateTotal()
        {
        }
        public CalculateTotal(decimal finalTotal,decimal subtotal, decimal tax) : base(subtotal, tax)
        {
            FinalTotal = finalTotal;
        }
        public decimal GetFinalTotal(decimal subtotal)
        {
            FinalTotal = Math.Round(subtotal + (subtotal * Tax),2);
            return FinalTotal;
        }
    }
}
