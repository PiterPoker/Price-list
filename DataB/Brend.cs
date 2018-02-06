using System.Collections.Generic;

namespace Test_Price.DataB
{
    public class Brend
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Product> Products { get; set; }
        public Brend()
        {
            Products = new List<Product>();
        }
    }
}
