using ClincaVeterinariaAspCore.Models;
using ClincaVeterinariaAspCore.Repositories.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClincaVeterinariaAspCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ILoginRepository _loginRepository;

        public HomeController(ILogger<HomeController> logger, ILoginRepository loginRepository)
        {
            _logger = logger;
            _loginRepository = loginRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult LoginPainel()
        //{
        //    return View();
        //}
        [HttpPost]
        public IActionResult Index(Login verLogin)
        {
            _loginRepository.TestarUsuario(verLogin);
            if (verLogin.usuario != null && verLogin.senha != null)
            {
                HttpContext.Session.SetString("Nome", verLogin.usuario);
                HttpContext.Session.SetString("Senha", verLogin.senha);
                HttpContext.Session.SetString("Tipo", verLogin.tipo);


                if (verLogin.tipo == "1")
                {
                    HttpContext.Session.SetString("Tipo", verLogin.tipo);//=1

                    ViewBag.UserLogado = HttpContext.Session.GetString("Nome");
                    ViewBag.UserTipo = HttpContext.Session.GetString("Tipo");

                    return RedirectToAction("PainelComum", "Home");
                }
                else
                {
                    HttpContext.Session.SetString("Tipo", verLogin.tipo);//=2

                    ViewBag.UserLogado = HttpContext.Session.GetString("Nome");
                    ViewBag.UserTipo = HttpContext.Session.GetString("Tipo");

                    return RedirectToAction("PainelAdmin", "Home");
                }
            }
            else
            {
                ViewBag.msgLogar = "Usuário não encontrado. Verifique o nome do usuário e a senha";
                return View();
            }
        }
        public IActionResult PainelAdmin()
        {
            ViewBag.UserLogado = HttpContext.Session.GetString("Nome");
            ViewBag.UserTipo = HttpContext.Session.GetString("Tipo");
            // return new ContentResult() { Content = "Este é o Painel do Cliente Admin!" + ViewBag.UsuarioLogado };
            return View();
        }
        public IActionResult PainelComum()
        {

            ViewBag.UserLogado = HttpContext.Session.GetString("Nome");
            ViewBag.UserTipo = HttpContext.Session.GetString("Tipo");

            //return new ContentResult() { Content = "Este é o Painel do Cliente Comum!" };
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public ActionResult Logout()
        {
            ViewBag.UserIdo = null;
            ViewBag.UserLogado = null;
            ViewBag.UserTipo = null;
            return RedirectToAction("LoginPainel", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}