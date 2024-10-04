using Fiap.Web.Donation3.Data;
using Fiap.Web.Donation3.Models;
using Fiap.Web.Donation3.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fiap.Web.Donation3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ProdutoRepository _produtoRepository;

        private readonly int UserId = 1;

        private readonly bool Autenticado = true;


        public HomeController(ILogger<HomeController> logger, DataContext dataContext)
        {
            _logger = logger;
            _produtoRepository = new ProdutoRepository(dataContext);
        }

        public IActionResult Index()
        {
            var produtos = new List<ProdutoModel>();

            if ( Autenticado )
            {
                produtos = _produtoRepository.FindAllAvaliableForChange(UserId);
            } else
            {
                produtos = _produtoRepository.FindAllAvaliable();
            }

            return View(produtos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
