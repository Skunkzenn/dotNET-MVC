using System.Collections.Generic;
using System;
using System.Linq;

namespace dotNetMVC.Models
{   //Criar classe e a seguir o controller DepartmentsController
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //utilizar tipo ICollection, pois ele aceita listas, HashSet... seguido de sua instânciação
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>(); //Associação aos vendedores!

        /*
        O framework precisa deste construtor, ele só é necessário, pois vamos
        implementar o construtor com argumentos, se nao existisse o construtor com argumentos
        o construtor vazio não seria necessário. */
        public Department()
        {

        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public void RemoveSeller(Seller seller)
        {
            Sellers.Remove(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            //pega-se em cada vendedor da lista, chama o total sales em cada vendedor
            //e efetua a soma total de todas as vendas
            return Sellers.Sum(sellers => sellers.TotalSales(initial, final));
        }
    }
}
