using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetMVC.Services.Exceptions
{
    public class DbConcurrencyException : ApplicationException
    {
        // Desta forma, temos um controle sobre como tratar cada tipo de excessão que pode ocorrer
        public DbConcurrencyException(string message) : base(message)
        {
        }
    }
}
