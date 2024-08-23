using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNetMVC.Services;

namespace dotNetMVC.Controllers
{
    public class SellersController : Controller
    {
        //declarar dependcia para o SellerService
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            //implementar a chamada do sellerservice findall
            var list = _sellerService.FindAll();
            return View(list);
        }
    }
}
