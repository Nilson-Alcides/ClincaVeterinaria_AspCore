using aspnetcoremvc_adonet.Models;
using aspnetcoremvc_adonet.Repositories.Contract;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcoremvc_adonet.Controllers
{
    public class TipoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ITipoAnimalRepository _tipoAnimalRepository;

        public TipoController(ILogger<HomeController> logger, ITipoAnimalRepository tipoAnimalRepository)
        {
            _logger = logger;
            _tipoAnimalRepository = tipoAnimalRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListaTipoAnimal()
        {
            return View(_tipoAnimalRepository.ObterTodosTipoAnimal());

        }

        public IActionResult CadTipo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadTipo(TipoAnimal tipoAnimal)
        {
            _tipoAnimalRepository.Cadastrar(tipoAnimal);
            ViewBag.Message = "Cadastro realizado com sucesso.";
            return RedirectToAction(nameof(ListaTipoAnimal));
        }

        public IActionResult editarTipo(int id)
        {

            return View(_tipoAnimalRepository.ObterTipoAnimal(id));
        }
        [HttpPost]
        public IActionResult editarTipo(TipoAnimal tipoAnimal)
        {
            _tipoAnimalRepository.Atualizar(tipoAnimal);
            return RedirectToAction(nameof(ListaTipoAnimal));
        }
        public IActionResult Delete(int id)
        {
            _tipoAnimalRepository.Excluir(id);
            return RedirectToAction(nameof(ListaTipoAnimal));
        }
    }
}
