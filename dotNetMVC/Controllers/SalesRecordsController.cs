using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotNetMVC.Data;
using dotNetMVC.Services;

namespace dotNetMVC.Controllers
{
    public class SalesRecordsController : Controller
    {

        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {       //se não houver um valor mínimo, vai receber o inicio do ano
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }   //se não houver um valor máximo, vai receber a data atual
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            //Aqui o controlador envia os dados para as box na view, de forma que as datas
            //fiquem a ser exibidas sempre que um utilizador realize a busca
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }

    }
}
