using System;

//ErrorViewModel n�o � um Model/Entidade do neg�cio
//� apenas um modelo auxiliar para povoar nossas telas
//Logo as classes que forem os view models(modelos auxiliares) cria-se uma pasta a parte, para organiza��o
//Ter aten��o ao name space para organizar corretamente o acesso a classe e sua localiza��o
namespace dotNetMVC.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}