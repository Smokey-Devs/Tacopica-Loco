using Microsoft.AspNetCore.Mvc;
using TacoPIca_loco.Models.ViewModels;
using TacoPIca_loco.Repositorio;

namespace TacoPIca_loco.Controllers
{
    public class SearchController : Controller
    {
        private readonly ProdutoRepositorio _cardapioRepositorio;
        private readonly FornecedorRepositorio _fornecedorRepositorio;
        private readonly FuncionarioRepositorio _funcionarioRepositorio;

        public SearchController(
            ProdutoRepositorio cardapioRepositorio,
            FuncionarioRepositorio funcionarioRepositorio,
            FornecedorRepositorio fornecedorRepositorio)
        {
            _cardapioRepositorio = cardapioRepositorio;
            _fornecedorRepositorio = fornecedorRepositorio;
            _funcionarioRepositorio = funcionarioRepositorio;
        }

        public IActionResult Search()
        {
            var model = new SearchViewModel
            {
                Pratos = _cardapioRepositorio.TodosProdutos(),
                Fornecedores = _fornecedorRepositorio.TodosFornecedores(),
                Funcionarios = _funcionarioRepositorio.TodosFuncionarios()
            };

            return View(model);
        }
    }
}