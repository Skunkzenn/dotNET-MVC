using System;
using System.Collections.Generic;
using System.Linq;

namespace dotNetMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; } //Associação ao departamento
        public int DepartmentId { get; set; } //Inclui o ID do departamento, indicando para o entity framework que o ID terá que existir na classe seller,
                                              //garantindo assim que o ID não será nulo. (tipo int não pode ser nulo!)
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>(); //Associação sob o SalesRecord, INSTÂNCIAR!!

        /*
        O framework precisa deste construtor, ele só é necessário, pois vamos
        implementar o construtor com argumentos, se nao existisse o construtor com argumentos
        o construtor vazio não seria necessário. */
        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        //Operação para adicionar venda na LISTA de vendas!!
        public void AddSales(SalesRecord salesRecord)
        {
            Sales.Add(salesRecord);
        }

        public void RemoveSales(SalesRecord salesRecord)
        {
            Sales.Remove(salesRecord);
        }

        //chama a lista de vendas associada ao vendedor, assimilar salesRecord ao .Date
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(salesRecord => salesRecord.Date >= initial && salesRecord.Date <= final).Sum(salesRecord => salesRecord.Amount);
        }
    }
}
