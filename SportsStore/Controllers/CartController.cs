using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.Repo;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IStoreRepository repository;
        private readonly Cart cart;

        public CartController(IStoreRepository repository, Cart cart)
        {
            this.repository = repository;
            this.cart = cart;
        }

        [HttpGet]
        public IActionResult Index(string returnUrl)
            => View(new CartViewModel{ ReturnUrl = returnUrl ?? "/" });

        [HttpPost]
        public IActionResult Index(long productId, string returnUrl)
        {
            Product product = this.repository.Products.FirstOrDefault(p => p.ProductId == productId);
            this.cart.AddItem(product, 1);
            return View(new CartViewModel
            {
                Cart = this.cart,
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public IActionResult Remove(long productId, string returnUrl)
        {
            this.cart.RemoveLine(this.cart.Lines.First(cl => cl.Product.ProductId == productId).Product);
            return View("Index", new CartViewModel
            {
                Cart = this.cart,
                ReturnUrl = returnUrl ?? "/"
            });
        }
    }
}
