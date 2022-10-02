namespace PYP_FtontToBack.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public double Rate { get; set; }
        public virtual ICollection<ProductPhoto> ProductPhotos { get; set; }
        public Product()
        {
            ProductPhotos=new HashSet<ProductPhoto>();
        }

    }
}
