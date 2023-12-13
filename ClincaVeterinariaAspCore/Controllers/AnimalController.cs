using ClincaVeterinariaAspCore.Models;
using ClincaVeterinariaAspCore.Repositories;
using ClincaVeterinariaAspCore.Repositories.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClincaVeterinariaAspCore.Controllers
{
    public class AnimalController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAnimalRepository _animalRepositoryy;
        private IRacaRepository _racaRepository;
        private IClienteRepository _clienteRepository;

        public AnimalController(ILogger<HomeController> logger, IRacaRepository racaRepository, IAnimalRepository animalRepository, IClienteRepository clienteRepository)
        {
            _logger = logger;
            _racaRepository = racaRepository;
            _animalRepositoryy = animalRepository;
            _clienteRepository = clienteRepository;
        }
        public IActionResult ListaAnimal()
        {
            return View(_animalRepositoryy.ObterTodosAnimais());
        }
        public IActionResult CadAnimal()
        {
            ViewBag.UserLogado = HttpContext.Session.GetString("Nome");
            ViewBag.Senha = HttpContext.Session.GetString("Senha");
            ViewBag.UserTipo = HttpContext.Session.GetString("Tipo");

            if (ViewBag.UserLogado  == null && ViewBag.Senha == null)
            {
                return RedirectToAction("Index", "Home");

            }else
            {
                //Seleciona os clientes ***Lista de Clientes***
                var listaCli = _clienteRepository.ObterTodosClientes();
                var ObjCliente = new Animal
                {
                    ListaCliente = (List<Cliente>)listaCli

                };
                ViewBag.ListaClientes = new SelectList(listaCli, "id", "nomeCli");

                //Seleciona as raças ***Lista de Raças***
                var listaRaca = _racaRepository.ObterTodosRaca();
                var ObjRaca = new Animal
                {
                    ListaRaca = (List<Raca>)listaRaca

                };
                ViewBag.ListaRacas = new SelectList(listaRaca, "Id", "racaAni");

                return View();

            }
            return View();

        }
        [HttpPost]
        public IActionResult CadAnimal(Animal animal)
        {
            //***Lista de Clientes***
            var listaCli = _clienteRepository.ObterTodosClientes();
            ViewBag.ListaClientes = new SelectList(listaCli, "Id", "nomeCli");
            //***Lista de Raças***
            var listaRaca = _racaRepository.ObterTodosRaca();
            ViewBag.ListaRacas = new SelectList(listaRaca, "Id", "racaAni");

            _animalRepositoryy.Cadastrar(animal);

            return RedirectToAction(nameof(ListaAnimal));
        }
        public IActionResult editarAnimal(int id)
        {
            //Seleciona os clientes ***Lista de Clientes***
            var listaCli = _clienteRepository.ObterTodosClientes();
            var ObjCliente = new Animal
            {
                ListaCliente = (List<Cliente>)listaCli

            };
            ViewBag.ListaClientes = new SelectList(listaCli, "id", "nomeCli");

            //Seleciona as raças ***Lista de Raças***
            var listaRaca = _racaRepository.ObterTodosRaca();
            var ObjRaca = new Animal
            {
                ListaRaca = (List<Raca>)listaRaca

            };
            ViewBag.ListaRacas = new SelectList(listaRaca, "Id", "racaAni");

            return View(_animalRepositoryy.ObterAnimais(id));
        }

        [HttpPost]
        public IActionResult editarAnimal(Animal animal)
        {
            //***Lista de Clientes***
            var listaCli = _clienteRepository.ObterTodosClientes();
            ViewBag.ListaClientes = new SelectList(listaCli, "id", "nomeCli");
            
            //***Lista de Raças***
            var listaRaca = _racaRepository.ObterTodosRaca();
            ViewBag.ListaRacas = new SelectList(listaRaca, "Id", "racaAni");

            _animalRepositoryy.Atualizar(animal);
            return RedirectToAction(nameof(ListaAnimal));
        }
        public IActionResult Delete(int id)
        {
            _animalRepositoryy.Excluir(id);
            return RedirectToAction(nameof(ListaAnimal));
        }
    }
}
