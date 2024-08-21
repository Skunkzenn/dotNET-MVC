using dotNetMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace dotNetMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Department> departments = new List<Department>();
            //Método de instanciamento automático
            departments.Add(new Department { Id = 1, Name = "Eletronics"});
            departments.Add(new Department { Id = 2, Name = "Fashion" });
            //Para retornar a lista, basta coloca-lá entre parênteses no método View(LISTA)
            //Criar sub-pasta Departments na pasta Views, lembrando que devemos seguir o nome do controller -> Departments, NÃO PODE FICAR ERRADO!
            return View(departments);
        }
    }
}
