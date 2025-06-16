using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TacoPIca_loco.Models;
using TacoPIca_loco.Repositorio;

namespace TacoPIca_loco.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly UsuarioRepositorio _usuarioRepositorio;
    
    public LoginController(ILogger<LoginController> logger, UsuarioRepositorio usuarioRepositorio)
    {
        _logger = logger;
        _usuarioRepositorio = usuarioRepositorio;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [HttpPost]
    public IActionResult Index(string email, string senha)
    {
        var usuario = _usuarioRepositorio.ObterUsuario(email);
        if (usuario != null && usuario.Email == email && usuario.Senha == senha)
        {
            return RedirectToAction("Home", "Home");
        }
        else
        {
            ViewBag.ErrorMessage = "Email ou senha inválidos.";
            return View("Index");
        }
    }
}