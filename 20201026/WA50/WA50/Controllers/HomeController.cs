﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind.Store.Data;
using Northwind.Store.Model;
using WA50.Models;
using WA50.ViewModels;

namespace WA50.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// /Home/Index
        /// </summary>
        //public IActionResult Index(string filter = "")
        public IActionResult Index(ProductIndexViewModel vm)
        {
            var filter = HttpContext.Session.GetString(nameof(vm.Filter));

            vm.Filter = (vm.Filter ?? filter) ?? "";
            HttpContext.Session.SetString(nameof(vm.Filter), vm.Filter);

            if (vm.Filter != filter)
            {
                vm.Page = 1;
            }

            var result = new List<Product>();
            using (var db = new NWContext())
            {
                //result = db.Products.Where(p=> p.ProductName.Contains(filter)).ToList();
                //result = db.Products.Where(p => p.ProductName.Contains(vm.Filter)).ToList();
                vm.TotalCount = db.Products.Where(p => p.ProductName.Contains(vm.Filter)).Count();

                result = db.Products.Where(p => p.ProductName.Contains(vm.Filter))
                    .OrderBy(p => p.ProductName).Skip((vm.Page - 1) * vm.PageSize).
                    Take(vm.PageSize).ToList();
            }

            //var vm = new ProductIndexViewModel() { Products = result };
            vm.Products = result;

            // Dictionary
            //ViewData["products"] = result;
            //ViewBag.productsD = result;

            return View(vm);
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
