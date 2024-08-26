using System;
using System.ComponentModel.DataAnnotations;
using dotNetMVC.Models.Enums;


namespace dotNetMVC.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Amount { get; set; }
        public SalesStatus Status { get; set; }
        public Seller Seller { get; set; }

        /*
        O framework precisa deste construtor, ele só é necessário, pois vamos
        implementar o construtor com argumentos, se nao existisse o construtor com argumentos
        o construtor vazio não seria necessário. */
        public SalesRecord()
        {

        }

        public SalesRecord(int id, DateTime date, double amount, SalesStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}
