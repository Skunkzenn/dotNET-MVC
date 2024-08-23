using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNetMVC.Data;
using dotNetMVC.Models;

namespace dotNetMVC.Services
{
    public class SellerService
    {   //previne que a dependência não seja alterada
        private readonly dotNetMVCContext _context;

        public SellerService(dotNetMVCContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {   //Acessa a fonte de dados da tabela vendedores e converte para uma lista
            return _context.Seller.ToList();
        }
    }
}
