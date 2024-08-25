using dotNetMVC.Data;
using dotNetMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        //Uso de chamada assíncrona
        //Define-se o async em todas as funções que fazem comunicação com a base de dados
        public async Task<List<Department>> FindAllAsync()
        {   //Acessa a fonte de dados da tabela vendedores e converte para uma lista
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }

    }
}
