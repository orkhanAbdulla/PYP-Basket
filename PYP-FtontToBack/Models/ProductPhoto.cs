using System.ComponentModel.DataAnnotations.Schema;

namespace PYP_FtontToBack.Models
{
    public class ProductPhoto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }=null!;
        public string Url { get; set; } = null!;
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
