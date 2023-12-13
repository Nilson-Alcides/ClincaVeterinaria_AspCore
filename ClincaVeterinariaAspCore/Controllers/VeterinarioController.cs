using ClincaVeterinariaAspCore.Models;
using ClincaVeterinariaAspCore.Repositories;
using ClincaVeterinariaAspCore.Repositories.Contract;
using Microsoft.AspNetCore.Mvc;

namespace ClincaVeterinariaAspCore.Controllers
{
    public class VeterinarioController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IVeterinarioRepository _veterinarioRepository;

        public VeterinarioController(ILogger<HomeController> logger, IVeterinarioRepository veterinarioRepository)
        {
            _logger = logger;
            _veterinarioRepository = veterinarioRepository;
        }
        public IActionResult ListaVeterinario()
        {
            return View(_veterinarioRepository.ObterTodosVeterinario());
        }
        public IActionResult CadVeterinario()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadVeterinario(Veterinario veterinario)
        {
            _veterinarioRepository.Cadastrar(veterinario);
            ViewBag.Message = "Cadastro realizado com sucesso.";
            return RedirectToAction(nameof(ListaVeterinario));
        }
        public IActionResult editarVeterinario(int id)
        {
            return View(_veterinarioRepository.ObterVeterinario(id));
        }
        [HttpPost]
        public IActionResult editarVeterinario(Veterinario veterinario)
        {
            _veterinarioRepository.Atualizar(veterinario);

            return RedirectToAction(nameof(ListaVeterinario));
        }
        public IActionResult Delete(int id)
        {
            _veterinarioRepository.Excluir(id);

            return RedirectToAction(nameof(ListaVeterinario));
        }
    }
}