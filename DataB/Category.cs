using System.Collections.Generic;

namespace Test_Price.DataB
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Brend> Brends { get; set; }
        public Category()
        {
            Brends = new List<Brend>();
        }
    }
}
