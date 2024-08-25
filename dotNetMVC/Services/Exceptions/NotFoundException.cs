using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetMVC.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        //Criamos exceções específicas para termos controle sobre a camada serviço
        //Quando temos uma excessão personalizada, temos a possibilidade de tratar exclusivamente essa excessão.
        //Controle maior sobre como tratar cada tipo de excessão que pode ocorrer
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
