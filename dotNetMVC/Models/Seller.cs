using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace dotNetMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
                                // nome do atributo!!!    
        [Required(ErrorMessage = "{0} required")] // Define que o atributo é obrigatório   //{0} pega automaticamente o nome do atributo
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}.")] //Tamanho maximo e min de caracteres
        public string Name { get; set; }                                                //2º parâmetro inserido e segundo parâmetro inserido na definição de StringLength

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        //Altera nome do objeto visualmente no display(front-end)
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "{0} required")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; } //Associação ao departamento

        [Display(Name = "Department")]
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
