using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


//Classe para tratar o erro de delete de um vendedor,
//o vendedor possuí registros e o bando de dados nao deixa isso acontecer,
//essa classe é para inicial o tratamento deste erro

//EXCESSÃO PERSONALIZADA DE SERVIÇO PARA ERROS DE INTEGRIDADE REFERENCIAL
namespace dotNetMVC.Services.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string message) : base(message)
        {
        }
    }
}
