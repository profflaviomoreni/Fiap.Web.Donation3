using Fiap.Web.Donation3.Data;
using Fiap.Web.Donation3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace Fiap.Web.Donation3.Controllers
{
    public class ProdutoController : Controller
    {

        private readonly DataContext _dataContext;

        public ProdutoController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        [HttpGet]
        public IActionResult Index()
        {

            // SELECT * FROM Produto
            var listaProdutos = _dataContext.Produtos.ToList();

            // Exibir a View de Listagem de Produtos
            return View(listaProdutos);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProdutoModel());
        }

        [HttpPost]
        public IActionResult Create(ProdutoModel produtoModel)
        {
            if ( ModelState.IsValid )
            {
                TempData["MensagemSucesso"] = $"Produto {produtoModel.Nome} cadastro com sucesso";
                return RedirectToAction(nameof(Index));
            } else
            {
                return View(produtoModel);
            }

            
        }


        [HttpGet]
        public IActionResult Editar(int id)
        {
            // SELECT * FROM TB_PROD WHERE ProdutoID = {id} 
            var produto = ListarProdutosMock().Where( p => p.ProdutoId == id).FirstOrDefault();

            return View(produto);
        }


        [HttpPost]
        public IActionResult Editar(ProdutoModel produtoModel)
        {
            var sucesso = true;
            var mensagemErro = "";

            if (string.IsNullOrEmpty(produtoModel.Nome))
            {
                mensagemErro += "Nome do produto é obrigatório.";
                sucesso = false;
            }

            if (string.IsNullOrEmpty(produtoModel.Descricao))
            {
                mensagemErro += " Descrição é obrigatório.";
                sucesso = false;
            }

            if ( ! sucesso )
            {
                ViewBag.MensagemErro = mensagemErro;
                return View(produtoModel);
            } else
            {
                TempData["MensagemSucesso"] = $"Produto {produtoModel.Nome} alterado com sucesso";
                return RedirectToAction(nameof(Index));
            }
            
        }


        //[HttpGet]
        //public IActionResult Index()
        //{
        //    // SELECT * FROM Produto
        //    var listaProdutos = ListarProdutosMock();

        //    // Dados do banco e enviando para View por ViewBag
        //    ViewBag.Produtos = listaProdutos;

        //    // Exibir a View de Listagem de Produtos
        //    return View();
        //}


        private List<ProdutoModel> ListarProdutosMock()
        {
            // SELECT * FROM produtos;

            var produtos = new List<ProdutoModel>{
                new ProdutoModel()
                {
                    ProdutoId = 1,
                    Nome = "Iphone 11",
                    CategoriaId = 1,
                    Disponivel = true,
                    DataExpiracao = DateTime.Now,
                },
                new ProdutoModel()
                {
                    ProdutoId = 2,
                    Nome = "Iphone 12",
                    CategoriaId = 2,
                    Disponivel = true,
                    DataExpiracao = DateTime.Now,
                },
                new ProdutoModel()
                {
                    ProdutoId = 3,
                    Nome = "Iphone 13",
                    CategoriaId = 1,
                    Disponivel = true,
                    DataExpiracao = DateTime.Now,
                },
                new ProdutoModel()
                {
                    ProdutoId = 4,
                    Nome = "Iphone 14",
                    CategoriaId = 1,
                    Disponivel = false,
                    DataExpiracao = DateTime.Now,
                },
            };

            return produtos;
        }

    }
}
