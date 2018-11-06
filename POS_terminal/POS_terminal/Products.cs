using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_terminal
{
    public class Products
    {
        public int ProductNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        
        
        public Products()
        {

        }

        public Products(int productNumber, string name, string description, double price)
        {
            Name = name;
            Description = description;
            Price = price;
            ProductNumber = productNumber;
        }
    }
    public enum Category
    {
        plant,
        supplies,
        assortment
    }
}
