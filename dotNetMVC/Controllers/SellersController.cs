using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNetMVC.Services;
using dotNetMVC.Models.ViewModels;
using dotNetMVC.Models;
using dotNetMVC.Services.Exceptions;

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

        //valor opcional no int de entrada
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var obj = _sellerService.FindById(id.Value);
            
            if (obj == null)
            {
                return NotFound();
            } 
            return View(obj);
        }

        //Recebe o objeto vendedor para ser criado o método POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Previne que a aplicação sofra ataques CSRF, quando alguem aproveita a nossa sessão de autenticação para enviar dados maliciosos
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            //Abrir tela de edição, mas para abrir a tela, é preciso povoar a caixa de seleção
            List<Department> departments = _departmentService.FindAll();
            //define a classe seller com o objeto que buscamos na base de dados
            //como estamos fazendo uma edição, vamos preencher já com os dados do objeto existente.
            //Logo também devemos passar o Departments que está na nossa ViewModel
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id) // o Id não pode ser diferente, do Id do url da requisição
            {
                return BadRequest();
            }
            try //Chamada update pode lançar exceções, por isso devemos tratar
            {
                _sellerService.Update(seller); //Atualiza o vendedor
                return RedirectToAction(nameof(Index)); //redireciona para a pagina inicial do crud que é o index
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }

    }
}
