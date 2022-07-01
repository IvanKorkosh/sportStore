using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportsStore.Models.Repo;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private const int PageSize = 4;

        private readonly IStoreRepository repository;

        public HomeController(ILogger<HomeController> logger, IStoreRepository repository)
        {  
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public ViewResult Index(string category, int productPage = 1)
         => View(new ProductsListViewModel
         {
             Products = this.repository.Products
                   .Where(p => category == null || p.Category == category)
                   .OrderBy(p => p.ProductId)
                   .Skip((productPage - 1) * PageSize)
                   .Take(PageSize),
             PagingInfo = new PagingInfo
             {
                 CurrentPage = productPage,
                 ItemsPerPage = PageSize,
                 TotalItems = category == null ?
                       this.repository.Products.Count() :
                       this.repository.Products.Where(e =>
                           e.Category == category).Count()
             },
             CurrentCategory = category
         });

        public IActionResult Privacy()
            => View();
    }
}
