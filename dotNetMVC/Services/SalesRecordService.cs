using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNetMVC.Data;
using dotNetMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace dotNetMVC.Services
{
    public class SalesRecordService
    {
        //previne que a dependência não seja alterada
        private readonly dotNetMVCContext _context;

        public SalesRecordService(dotNetMVCContext context)
        {
            _context = context;
        }


        //Função para buscar vendas em um intervalo de tempo
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            //Pega no SalesRecord que é do tipo DbSet e construir um obj do tipo result IQueryable
            //E assim, em cima desse objeto podemos acrescentar detalhes sobre a consulta
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            //Pega no SalesRecord que é do tipo DbSet e construir um obj do tipo result IQueryable
            //E assim, em cima desse objeto podemos acrescentar detalhes sobre a consulta
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department) // Agrupa pelo departamento, Task<List<IGrouping<Department, SalesRecord>>>
                                                   // Quando se agrupa resultados, o tipo de retorno não será mais uma lista e sim os resultados ficam em uma coleção chamada IGrouping
                .ToListAsync();
        }
    }
}
