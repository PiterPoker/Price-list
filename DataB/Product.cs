using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test_Price.DataB
{
    public class Product
    {
        public int Id { get; set; }
        public string Country { get; set; } 
        public string Model { get; set; }
        public decimal Price { get; set; }

        public int? BrendId { get; set; }
        public Brend Brend { get; set; }    

        public List<Option> Options { get; set; }
        public Product()    
        {
            Options = new List<Option>();
        }
    }
}
