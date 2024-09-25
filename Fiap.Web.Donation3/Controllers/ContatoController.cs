using Fiap.Web.Donation3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.Donation3.Controllers
{
    public class ContatoController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            Console.WriteLine("Método Index");
            //return Content("Flávio");
            //return View();

            //return RedirectToAction(nameof(Help));
            return View();
        }


        [HttpPost]
        public IActionResult Index(ContatoModel contatoModel)
        {
            // INSERT INTO CONTATO VALUES ( contatoModel.nome, ... );


            return View();
        }



        /*
        [HttpPost]
        public IActionResult Gravar(ContatoModel contatoModel) { 

            // INSERT INTO CONTATO VALUES ( contatoModel.nome, ... );

            return View();
        }
        */


        [HttpGet]
        public IActionResult Help()
        {
            Console.WriteLine("Método Help");
            return View();
        }

    }
}
