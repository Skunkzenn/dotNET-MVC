using System;

//ErrorViewModel não é um Model/Entidade do negócio
//É apenas um modelo auxiliar para povoar nossas telas
//Logo as classes que forem os view models(modelos auxiliares) cria-se uma pasta a parte, para organização
//Ter atenção ao name space para organizar corretamente o acesso a classe e sua localização
namespace dotNetMVC.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}