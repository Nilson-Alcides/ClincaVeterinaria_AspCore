using ClincaVeterinariaAspCore.Models;
using ClincaVeterinariaAspCore.Repositories;
using ClincaVeterinariaAspCore.Repositories.Contract;
using Microsoft.AspNetCore.Mvc;

namespace ClincaVeterinariaAspCore.Controllers
{
    public class ClienteController : Controller
    {
     
        private IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {            
            _clienteRepository = clienteRepository;
        }
       
        public IActionResult ListaCliente()
        {
            return View(_clienteRepository.ObterTodosClientes());
        }
        
        public IActionResult CadCliente()
        {
            if (HttpContext.Session.Id == null || HttpContext.Session.IsAvailable != true)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                ViewBag.UserLogado = HttpContext.Session.GetString("Nome");
                ViewBag.UserTipo = HttpContext.Session.GetString("Tipo");
                return View();
            }
            
        }
        [HttpPost]
        public IActionResult CadCliente(Cliente cliente )
        {
            if (HttpContext.Session.Id == null || HttpContext.Session.IsAvailable != true)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                ViewBag.UserLogado = HttpContext.Session.GetString("Nome");
                ViewBag.UserTipo = HttpContext.Session.GetString("Tipo");


                _clienteRepository.Cadastrar(cliente);
                ViewBag.Message = "Cadastro realizado com sucesso.";
                return RedirectToAction(nameof(ListaCliente));
            }
        }
        public IActionResult editarCliente(int id)
        {
            return View( _clienteRepository.ObterCliente(id));
        }
        [HttpPost]
        public IActionResult editarCliente(Cliente cliente)
        {
            _clienteRepository.Atualizar(cliente);

            return RedirectToAction(nameof(ListaCliente));
        }
        public IActionResult Delete(int id)
        {
            _clienteRepository.Excluir(id);
            return RedirectToAction(nameof(ListaCliente));
        }


    }
}
