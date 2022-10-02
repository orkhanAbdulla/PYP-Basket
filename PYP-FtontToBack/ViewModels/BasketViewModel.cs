namespace PYP_FtontToBack.ViewModels
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string Image { get; set; } = null!;
        public decimal Price { get; set; }
        public int Count { get; set; } = 1;
    }
}
