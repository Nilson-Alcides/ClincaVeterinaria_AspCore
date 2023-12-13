using ClincaVeterinariaAspCore.Models;
using ClincaVeterinariaAspCore.Repositories;
using ClincaVeterinariaAspCore.Repositories.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySqlX.XDevAPI;

namespace ClincaVeterinariaAspCore.Controllers
{
    public class RacaController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IRacaRepository _racaRepository;
        private ITipoAnimalRepository _tipoAnimalRepository;

        public RacaController(ILogger<HomeController> logger, IRacaRepository racaRepository, ITipoAnimalRepository tipoAnimalRepository)
        {
            _logger = logger;
            _racaRepository = racaRepository;
            _tipoAnimalRepository = tipoAnimalRepository;
        }
        
        public IActionResult ListaRaca()
        {
            return View(_racaRepository.ObterTodosRaca());
        }
        public IActionResult CadRaca()
        {
           
            var listaTipo = _tipoAnimalRepository.ObterTodosTipoAnimal();
            var ObjTipo = new Raca
            {
                ListaTipo = (List<TipoAnimal>)listaTipo
            };
            ViewBag.Lista = new SelectList(listaTipo, "Id", "Tipo");

            return View();

        }
        [HttpPost]
        public IActionResult CadRaca(Raca raca)
        {
            var listaTipo = _tipoAnimalRepository.ObterTodosTipoAnimal();
            ViewBag.Lista = new SelectList(listaTipo, "Id", "Tipo");

            _racaRepository.Cadastrar(raca);
         
            return RedirectToAction(nameof(ListaRaca));
        }
        public IActionResult editarRaca(int id)
        {
            var listaTipo = _tipoAnimalRepository.ObterTodosTipoAnimal();
            var ObjTipo = new Raca
            {
                ListaTipo = (List<TipoAnimal>)listaTipo
            };
            ViewBag.Lista = new SelectList(listaTipo, "Id", "Tipo");

            return View(_racaRepository.ObterRaca(id));
        }
        [HttpPost]
        public IActionResult editarRaca(Raca raca)
        {
            var listaTipo = _tipoAnimalRepository.ObterTodosTipoAnimal();
            ViewBag.Lista = new SelectList(listaTipo, "Id", "Tipo");

            _racaRepository.Atualizar(raca);
            return RedirectToAction(nameof(ListaRaca));
        }
        public IActionResult Delete(int id)
        {
            _racaRepository.Excluir(id);
            return RedirectToAction(nameof(ListaRaca));
        }
    }
}
