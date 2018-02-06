using System.Collections.Generic;

namespace Test_Price.DataB
{
    public class Option
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public int? ProductId { get; set; } 
        public Product Product { get; set; }

        //public List<Product> Products { get; set; } 
        //public Option()
        //{
        //    Products = new List<Product>();
        //}
    }
}
