using Fiap.Web.Donation3.Data;
using Fiap.Web.Donation3.Models;
using Fiap.Web.Donation3.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.Donation3.Controllers
{
    public class TrocaController : Controller
    {

        private readonly int UserId = 1; 

        private readonly ProdutoRepository _produtoRepository;

        private readonly TrocaRepository _trocaRepository;

        public TrocaController(DataContext dataContext)
        {
            _produtoRepository = new ProdutoRepository(dataContext);        

            _trocaRepository = new TrocaRepository(dataContext);
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var produtoEscolhido = _produtoRepository.FindById(id);

            var trocaModel = new TrocaModel();
            trocaModel.ProdutoEscolhido = produtoEscolhido;

            var meusProdutos = _produtoRepository.FindAllAvaliableByUser(UserId);
            ViewBag.MeusProdutos = meusProdutos;

            return View(trocaModel);
        }


        [HttpPost]
        public IActionResult Index(TrocaModel trocaModel)
        {
            var produtoEscolhido = _produtoRepository.FindById(trocaModel.ProdutoIdEscolhido);
            var produtoMeu = _produtoRepository.FindById(trocaModel.ProdutoIdMeu);

            try
            {

                if ( produtoEscolhido.Disponivel == false )
                {
                    throw new Exception("Produto escolhida não mais disponível");
                }

                if (produtoMeu.Disponivel == false)
                {
                    throw new Exception("O seu produto foi utilizado em outra troca");
                }

                //if ( produtoMeu.Valor / produtoEscolhido.Valor < 0.9 )
                //{
                //    throw new Exception("O valor do seu produto é abaixo do esperado para troca");
                //}

                produtoEscolhido.Disponivel = false;
                _produtoRepository.Update(produtoEscolhido);

                produtoMeu.Disponivel = false;
                _produtoRepository.Update(produtoMeu);

                trocaModel.TrocaStatus = TrocaStatus.Iniciado;
                _trocaRepository.Insert(trocaModel);

                TempData["MensagemSucesso"] = "Troca efetivada com sucesso";

            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Problema na troca: {ex.Message}";
            }

            return RedirectToAction(nameof(Index),"Home");
        }


    }
}
