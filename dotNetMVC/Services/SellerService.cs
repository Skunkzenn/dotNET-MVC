using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNetMVC.Data;
using dotNetMVC.Models;
using Microsoft.EntityFrameworkCore;
using dotNetMVC.Services.Exceptions;

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

        //Atualizar o obj do tipo seller
        public void Update(Seller obj)
        {
            //Teste de verificação no banco de dados para confirmar se existe o vendedor para ser atualizado
            if(!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new DllNotFoundException("Id not found");
            }
            //Quando chamamos a operação de atualizar no banco de dados
            //o banco de dados pode retornar uma excessão de conflitos de concorrência
            //Logo, devemos tratar isso, utilizando a Exception que criamos para capturar o erro
            
            try {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e) //Estamos interceptando uma excessão do nivel de acesso a dados
            {   //E relançando a excessão, só que em nível de serviço, sendo muito importante para segregar as camadas
                //E assim o controlador(sellerscontroller) vai ter que lidar somente com excessões da camada de serviço,
                //respeitando a arquitetura que foi proposta a se fazer
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
