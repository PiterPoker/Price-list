using System.Data.Entity;

namespace Test_Price.DataB
{
    class PriceContext : DbContext
    {
        public PriceContext() : base("PricesContext")
        { }

        public DbSet<Category> Categorys { get; set; }  
        public DbSet<Product> Products { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Brend> Brends { get; set; }
    }
}
