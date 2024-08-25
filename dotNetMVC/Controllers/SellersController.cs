﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
            if (!ModelState.IsValid) // Validação para não ser preenchido o formulário em branco e ser aceite no banco de dados, pois quando o javascript esta desabilitado no browser, poderia-se sofrer este erro.
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments }
                return View(viewModel); // No caso, repasa-se a view do objeto para que seja completado.
            }
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        //valor opcional no int de entrada
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            
            var obj = _sellerService.FindById(id.Value);
            
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
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
                return RedirectToAction(nameof(Error), new { message = "Id not provided" }); ;
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
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
            if (!ModelState.IsValid) // Validação para não ser preenchido o formulário em branco e ser aceite no banco de dados
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments}
                return View(viewModel); // No caso, repasa-se a view do objeto para que seja completado.
            }
            if (id != seller.Id) // o Id não pode ser diferente, do Id do url da requisição
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try //Chamada update pode lançar exceções, por isso devemos tratar
            {
                _sellerService.Update(seller); //Atualiza o vendedor
                return RedirectToAction(nameof(Index)); //redireciona para a pagina inicial do crud que é o index
            }//Como estamos a tratar as possiveis excessões, as excessões carregam uma mensagem, logo passamos ela como parâmetro de entrada
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

            /*
             Ou podemos apagar as duas exceções e deixar apenas o super tipo delas
             catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }             
            */
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier //Massete do framework para pegar o ID interno da requisição 
            };
            return View(viewModel);
        }
    }
}
