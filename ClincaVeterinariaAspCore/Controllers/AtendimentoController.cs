using ClincaVeterinariaAspCore.Models;
using ClincaVeterinariaAspCore.Repositories;
using ClincaVeterinariaAspCore.Repositories.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClincaVeterinariaAspCore.Controllers
{
    public class AtendimentoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAtendimentoRepository _atendimentoRepository;
        private IAnimalRepository _animalRepository;
        private IVeterinarioRepository _veterinarioRepository;
        
        

        public AtendimentoController(ILogger<HomeController> logger, IAtendimentoRepository atendimentoRepository, IAnimalRepository animalRepository, IVeterinarioRepository veterinarioRepository )
        {
            _logger = logger;
            _atendimentoRepository = atendimentoRepository;
            _animalRepository = animalRepository;
            _veterinarioRepository = veterinarioRepository;
        }

        public IActionResult ListaAtendimento()
        {
           return View(_atendimentoRepository.ObterTodosAtendimentos());
        }

        public IActionResult CadAtendimento()
        {
            //Seleciona os clientes ***Lista de Clientes***
            var listaVeter = _veterinarioRepository.ObterTodosVeterinario();
            var ObjAtendimento = new Atendimento
            {
                ListaVeterinario = (List<Veterinario>)listaVeter

            };
            ViewBag.ListaVeterinarios = new SelectList(listaVeter, "Id", "NomeVeter");

            //Seleciona as raças ***Lista de Raças***
            var listaAnimais = _animalRepository.ObterTodosAnimais();
            var ObjAnimal = new Atendimento
            {
                ListaAnimal = (List<Animal>)listaAnimais

            };
            ViewBag.ListaAnimais = new SelectList(listaAnimais, "Id", "nomeAni");

            return View();

        }
        [HttpPost]
        public IActionResult CadAtendimento(Atendimento atendimento)
        {
            //***Lista de veterinarios***
            var listaVeter = _veterinarioRepository.ObterTodosVeterinario();
            ViewBag.ListaVeterinarios = new SelectList(listaVeter, "Id", "NomeVeter");
            //***Lista de animais***
            var listaAnimais = _animalRepository.ObterTodosAnimais();
            ViewBag.ListaAnimais = new SelectList(listaAnimais, "Id", "nomeAni");

            _atendimentoRepository.Cadastrar(atendimento);

            return RedirectToAction(nameof(ListaAtendimento));
        }
        public IActionResult CancelarAtendimento(int id)
        {
            return View(_atendimentoRepository.ObterAtendimentos(id));
        }
            // Cancelar Atendimento
            [HttpPost]
        public IActionResult CancelarAtendimento(Atendimento atendimento)
        {
            //***Lista de veterinarios***
            var listaVeter = _veterinarioRepository.ObterTodosVeterinario();
            ViewBag.ListaVeterinarios = new SelectList(listaVeter, "Id", "NomeVeter");
            //***Lista de animais***
            var listaAnimais = _animalRepository.ObterTodosAnimais();
            ViewBag.ListaAnimais = new SelectList(listaAnimais, "Id", "nomeAni");

            _atendimentoRepository.CancelarAtend(atendimento);
            return RedirectToAction(nameof(ListaAtendimento));
        }
    }
}
