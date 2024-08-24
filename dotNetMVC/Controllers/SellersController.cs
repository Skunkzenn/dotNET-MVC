using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNetMVC.Services;
using dotNetMVC.Models.ViewModels;
using dotNetMVC.Models;

namespace dotNetMVC.Controllers
{
    public class SellersController : Controller
    {
        //declarar dependcia para o SellerService
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            //implementar a chamada do sellerservice findall
            var list = _sellerService.FindAll();
            return View(list);
        }

        //Ação para criação de novo cadastro de vendedor
        //Método que abre a página para cadastrar um vendedor
        public IActionResult Create()
        {
            //passa o objeto carregado com os departamentos para a viewModel 
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        //Recebe o objeto vendedor para ser criado o método POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Previne que a aplicação sofra ataques CSRF, quando alguem aproveita a nossa sessão de autenticação para enviar dados maliciosos
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}
