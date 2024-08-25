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

        //Define-se o async em todas as funções que fazem comunicação com a base de dados

        public async Task<List<Seller>> FindAllAsync()
        {   //Acessa a fonte de dados da tabela vendedores e converte para uma lista
            return await _context.Seller.ToListAsync();
        }

        //Método para inserir o vendedor no banco de dados
        public async Task InsertAsync(Seller obj)
        {
            //obj.Department = _context.Department.First(); //Pega o primeiro departamento disponivel no banco de dados e associa com o vendedor que está a ser criado
            _context.Add(obj);
            await _context.SaveChangesAsync(); 
        }

        //Busca vendedor pelo ID
        public async Task<Seller> FindByIdAsync(int id)
        {   //Carrega os objetos relacionados com o objeto principal, no caso o vendedor possuí um departamento(outra classe)...
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try { 
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e) //Tratamento da execessão de deleção de um vendedor com registros de vendas
            {
                throw new IntegrityException("Can't delete seller because he/she has sales");
            }
        }

        //Atualizar o obj do tipo seller
        public async Task UpdateAsync(Seller obj)
        {
            //Teste de verificação no banco de dados para confirmar se existe o vendedor para ser atualizado
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if(!hasAny)
            {
                throw new DllNotFoundException("Id not found");
            }
            //Quando chamamos a operação de atualizar no banco de dados
            //o banco de dados pode retornar uma excessão de conflitos de concorrência
            //Logo, devemos tratar isso, utilizando a Exception que criamos para capturar o erro
            
            try {
                _context.Update(obj);
                await _context.SaveChangesAsync();
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
