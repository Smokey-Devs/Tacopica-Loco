using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TacoPIca_loco.Models;
using TacoPIca_loco.Repositorio;

namespace TacoPIca_loco.Controllers
{
    public class AddController : Controller
    {
        private readonly ProdutoRepositorio _produtoRepositorio;
        private readonly FuncionarioRepositorio _funcionarioRepositorio;
        private readonly FornecedorRepositorio _fornecedorRepositorio;

        public AddController(
            ProdutoRepositorio produtoRepositorio,
            FuncionarioRepositorio funcionarioRepositorio,
            FornecedorRepositorio fornecedorRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
            _funcionarioRepositorio = funcionarioRepositorio;
            _fornecedorRepositorio = fornecedorRepositorio;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var tipo = TempData["TipoEdicao"]?.ToString(); // Mantém disponível para o próximo request também
            var json = TempData["Item"]?.ToString();

            ViewBag.TipoEdicao = tipo;
            ViewBag.ItemEdicao = json;
            return View();
        }

        [HttpPost]
        [HttpPost]
        public IActionResult AdicionarPrato(Cardapio prato)
        {
            if (prato.IdPrato > 0)
            {
                _produtoRepositorio.EditarProduto(prato);
            }
            else
            {
                _produtoRepositorio.AdicionarProduto(prato);
            }

            return RedirectToAction("Search", "Search");
        }

        [HttpPost]
        public IActionResult AdicionarFuncionario(Funcionario funcionario)
        {
            if (funcionario.IdFuncionario > 0)
            {
                _funcionarioRepositorio.EditarFuncionario(funcionario);
            }
            else
            {
                funcionario.IdUsuario = 1; // Ou de onde vier esse valor
                _funcionarioRepositorio.AdicionarFuncionario(funcionario);
            }

            return RedirectToAction("Search", "Search");
        }

        [HttpPost]
        public IActionResult AdicionarFornecedor(Fornecedor fornecedor)
        {
            if (fornecedor.IdFornecedor > 0)
            {
                _fornecedorRepositorio.EditarFornecedor(fornecedor);
            }
            else
            {
                _fornecedorRepositorio.AdicionarFornecedor(fornecedor);
            }

            return RedirectToAction("Search", "Search");
        }
        [HttpGet]
        public IActionResult EditarPrato(int id)
        {
            var prato = _produtoRepositorio.ObterProduto(id);
            TempData["TipoEdicao"] = "Prato";
            TempData["Item"] = JsonConvert.SerializeObject(prato); // ou System.Text.Json
            return RedirectToAction("Add");
        }
        [HttpGet]
        public IActionResult EditarFuncionario(int id)
        {
            var func = _funcionarioRepositorio.ObterFuncionario(id);
            TempData["TipoEdicao"] = "Funcionario";
            TempData["Item"] = JsonConvert.SerializeObject(func);
            return RedirectToAction("Add");
        }
        [HttpGet]
        public IActionResult EditarFornecedor(int id)
        {
            var forn = _fornecedorRepositorio.ObterFornecedor(id);
            TempData["TipoEdicao"] = "Fornecedor";
            TempData["Item"] = JsonConvert.SerializeObject(forn);
            return RedirectToAction("Add");
        }

    }
}
