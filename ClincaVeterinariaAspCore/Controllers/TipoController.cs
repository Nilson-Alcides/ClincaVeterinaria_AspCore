using ClincaVeterinariaAspCore.Models;
using ClincaVeterinariaAspCore.Repositories.Contract;
using Microsoft.AspNetCore.Mvc;

namespace ClincaVeterinariaAspCore.Controllers
{
    public class TipoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ITipoAnimalRepository _tipoAnimalRepository;

        public c(ILogger<HomeController> logger, ITipoAnimalRepository tipoAnimalRepository)
        {
            _logger = logger;
            _tipoAnimalRepository = tipoAnimalRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        // Lista Tipo Animal
        public IActionResult ListaTipoAnimal()
        {
            return View(_tipoAnimalRepository.ObterTodosTipoAnimal());

        }
        // cadastro 
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
        //edtar
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
        //deletar
        public IActionResult Delete(int id)
        {
            _tipoAnimalRepository.Excluir(id);
            return RedirectToAction(nameof(ListaTipoAnimal));
        }
    }
}
