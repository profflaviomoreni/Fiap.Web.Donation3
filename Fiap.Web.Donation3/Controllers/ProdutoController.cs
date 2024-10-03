using Fiap.Web.Donation3.Data;
using Fiap.Web.Donation3.Models;
using Fiap.Web.Donation3.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace Fiap.Web.Donation3.Controllers
{
    public class ProdutoController : Controller
    {

        // Gambiarra do Flavio
        private readonly int UserId = 1;

        private readonly ProdutoRepository _produtoRepository;
        private readonly CategoriaRepository _categoriaRepository;

        public ProdutoController(DataContext dataContext)
        {
            _produtoRepository = new ProdutoRepository(dataContext);
            _categoriaRepository = new CategoriaRepository(dataContext);
        }


        [HttpGet]
        public IActionResult Index()
        {

            // SELECT * FROM Produto
            var listaProdutos = _produtoRepository.FindAll();

            // Exibir a View de Listagem de Produtos
            return View(listaProdutos);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categorias = _categoriaRepository.FindAll();

            return View(new ProdutoModel());
        }

        [HttpPost]
        public IActionResult Create(ProdutoModel produtoModel)
        {
            if ( ModelState.IsValid )
            {
                produtoModel.UsuarioId = UserId;
                _produtoRepository.Insert(produtoModel);

                TempData["MensagemSucesso"] = $"Produto {produtoModel.Nome} cadastro com sucesso";
                return RedirectToAction(nameof(Index));
            } else
            {
                ViewBag.Categorias = _categoriaRepository.FindAll();
                return View(produtoModel);
            }

            
        }


        [HttpGet]
        public IActionResult Editar(int id)
        {
            var produto = _produtoRepository.FindById(id);
            ViewBag.Categorias = _categoriaRepository.FindAll();
            return View(produto);
        }


        [HttpPost]
        public IActionResult Editar(ProdutoModel produtoModel)
        {
            if ( ModelState.IsValid )
            {
                produtoModel.UsuarioId = UserId;
                _produtoRepository.Update(produtoModel);

                TempData["MensagemSucesso"] = $"Produto {produtoModel.Nome} alterado com sucesso";
                return RedirectToAction(nameof(Index));
            } else
            {
                ViewBag.Categorias = _categoriaRepository.FindAll();
                ViewBag.MensagemErro = "Preencha todos os dados corretamente";
                return View(produtoModel);
            }
            
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            _produtoRepository.Delete(id);

            TempData["MensagemSucesso"] = $"Produto removido com sucesso";
            return RedirectToAction(nameof(Index));
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
