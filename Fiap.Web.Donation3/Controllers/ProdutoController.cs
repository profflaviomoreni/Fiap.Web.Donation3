using Fiap.Web.Donation3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.Donation3.Controllers
{
    public class ProdutoController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            // SELECT * FROM Produto
            var listaProdutos = ListarProdutosMock();

            // Exibir a View de Listagem de Produtos
            return View(listaProdutos);
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
            if ( string.IsNullOrEmpty(produtoModel.Nome) )
            {

                ViewBag.MensagemErro = "O campo nome é obrigatório";
                
                return View(produtoModel);

            } else
            {
                // UPDATE Produto SET .... WHERE ProdutoId = {produtoModel.id}

                // Criar a mensagem de sucesso;

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
