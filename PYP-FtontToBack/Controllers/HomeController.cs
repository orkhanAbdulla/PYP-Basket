using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.ContentModel;
using PYP_FtontToBack.DAL;
using PYP_FtontToBack.Models;
using PYP_FtontToBack.ViewModels;
using System.Diagnostics;

namespace PYP_FtontToBack.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View(_context.Products.Include(x=>x.ProductPhotos).AsQueryable());
        }
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id==null)
                return BadRequest();
            var product = await _context.Products.Include(x=>x.ProductPhotos).FirstOrDefaultAsync(x=>x.Id==id);
            if (product == null)
                return NotFound();
            List<BasketViewModel> basketViewModels;
            string existedBasket = HttpContext.Session.GetString("Basket");
            if (string.IsNullOrEmpty(existedBasket))
            {
                basketViewModels = new List<BasketViewModel>();
            }
            else
            {
                basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(existedBasket);
            }
            var existBsketViewModel = basketViewModels.FirstOrDefault(x => x.Id == id);
            if (existBsketViewModel == null)
            {
                existBsketViewModel = new BasketViewModel()
                {
                    Id = product.Id,
                };
                basketViewModels.Add(existBsketViewModel);
            }
            else
            {
                existBsketViewModel.Count++;
            }
            HttpContext.Session.SetString("Basket", JsonConvert.SerializeObject(basketViewModels));
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RemoveItem(int id)
        {
            if (id == 0) return NotFound();
            string existedBasket = HttpContext.Session.GetString("Basket");
            if (!string.IsNullOrEmpty(existedBasket))
            {
                List<BasketViewModel> basket = JsonConvert.DeserializeObject<List<BasketViewModel>>(existedBasket);
                Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (product == null) return NotFound();
                var existBsketViewModel = basket.FirstOrDefault(x => x.Id == id);
                if (existBsketViewModel != null)
                {
                    existBsketViewModel.Count--;
                    basket.Remove(existBsketViewModel);
                    HttpContext.Session.SetString("Basket", JsonConvert.SerializeObject(basket));
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}