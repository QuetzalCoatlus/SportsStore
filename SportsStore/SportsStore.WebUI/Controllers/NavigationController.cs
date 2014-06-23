using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SportsStore.Domain.Abstract;

namespace SportsStore.WebUI.Controllers
{
    public class NavigationController : Controller
    {
        IProductRepository repository;

        public NavigationController(IProductRepository productRepository)
        {
            repository = productRepository;
        }

        public PartialViewResult Menu()
        {

            IEnumerable<string> categories = repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(categories);
        }
    }
}
