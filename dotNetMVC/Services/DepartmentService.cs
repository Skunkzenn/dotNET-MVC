using dotNetMVC.Data;
using dotNetMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace dotNetMVC.Services
{
    public class DepartmentService
    {
        //previne que a dependência não seja alterada
        private readonly dotNetMVCContext _context;

        public DepartmentService(dotNetMVCContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {   //Acessa a fonte de dados da tabela vendedores e converte para uma lista
            return _context.Department.OrderBy(x => x.Name).ToList();
        }

    }
}
