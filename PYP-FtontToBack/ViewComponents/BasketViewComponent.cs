using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PYP_FtontToBack.DAL;
using PYP_FtontToBack.Models;
using PYP_FtontToBack.ViewModels;

namespace PYP_FtontToBack.ViewComponents
{
    public class BasketViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly AppDbContext _context;

        public BasketViewComponent(IHttpContextAccessor accessor, AppDbContext context)
        {
            _accessor = accessor;
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var basket = _accessor.HttpContext.Session.GetString("Basket");
            if (!string.IsNullOrEmpty(basket))
            {
                var basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(basket);
                var newBasket = new List<BasketViewModel>();
              
                foreach (var basketViewModel in basketViewModels)
                {
                    var product = _context.Products.Include(x => x.ProductPhotos).FirstOrDefault(x=>x.Id== basketViewModel.Id);
                    if (product == null)
                        continue;
                    newBasket.Add(new BasketViewModel
                    {
                        Id = product.Id,
                        ProductName=product.ProductName,
                        Price=product.Price,
                        Image= product.ProductPhotos.ElementAt(0).Url,
                        Count=basketViewModel.Count,
                    });
                }
              _accessor.HttpContext.Session.SetString("Basket", JsonConvert.SerializeObject(newBasket));
               return View(newBasket);
            }
            return View(basket);
        }
    }
}
