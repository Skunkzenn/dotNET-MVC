using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNetMVC.Data;
using dotNetMVC.Models;
using Microsoft.EntityFrameworkCore;

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

        //Método para inserir o vendedor no banco de dados
        public void Insert(Seller obj)
        {
            //obj.Department = _context.Department.First(); //Pega o primeiro departamento disponivel no banco de dados e associa com o vendedor que está a ser criado
            _context.Add(obj);
            _context.SaveChanges();
        }

        //Busca vendedor pelo ID
        public Seller FindById(int id)
        {   //Carrega os objetos relacionados com o objeto principal, no caso o vendedor possuí um departamento(outra classe)...
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
