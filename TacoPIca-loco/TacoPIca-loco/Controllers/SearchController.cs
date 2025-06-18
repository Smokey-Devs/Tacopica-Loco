using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public IActionResult ApagarProduto(int id)
        {
            _cardapioRepositorio.ApagarProduto(id);
            return RedirectToAction("Search");
        }
        
        public IActionResult ApagarFuncionario(int id)
        {
            _funcionarioRepositorio.ApagarFuncionario(id);
            return RedirectToAction("Search");
        }

        public IActionResult ApagarFornecedor(int id)
        {
            _fornecedorRepositorio.ApagarFornecedor(id);
            return RedirectToAction("Search");
        }
        
        public IActionResult EditarPrato(int id)
        {
            var prato = _cardapioRepositorio.ObterProduto(id);

            TempData["TipoEdicao"] = "Prato";
            TempData["Item"] = JsonConvert.SerializeObject(prato);

            return RedirectToAction("Add", "Add");
        }
        
        public IActionResult EditarFuncionario(int id)
        {
            var funcionario = _funcionarioRepositorio.ObterFuncionario(id); 

            TempData["TipoEdicao"] = "funcionario";
            TempData["Item"] = JsonConvert.SerializeObject(funcionario);

            return RedirectToAction("Add", "Add");
        }
        
        public IActionResult EditarFornecedor(int id)
        {
            var fornecedor = _fornecedorRepositorio.ObterFornecedor(id); 

            TempData["TipoEdicao"] = "Fornecedor";
            TempData["Item"] = JsonConvert.SerializeObject(fornecedor);

            return RedirectToAction("Add", "Add");
        }
    }
}