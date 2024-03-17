using CollegeKids2._0.Models;
using CollegeKids2._0.NewFolder;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace CollegeKids2._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Nosotros()
        {
            return View();
        }

        public IActionResult Contactanos()
        {
            return View();
        }
        public IActionResult Matricula()
        {
            return View();
        }
        public IActionResult Propuesta()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[Route("RegistrarPersona")]
        //[HttpPost]
        //public ActionResult RegistrarPersona(FormularioRequest registrar)
        //{

        //    LoginResponse sesionActual = (LoginResponse)Session["sesion"];
        //    if (sesionActual == null) return Json(JsonRequestBehavior.AllowGet);

        //    var PersonaClient = new MantenedorModel();

        //    PersonaClient._token = sesionActual.access_token;

        //    var resultadoApi = PersonaClient.RegistrarPersona(registrar);


        //    if (resultadoApi.codeHTTP == HttpStatusCode.Created)
        //    {
        //        Request.RequestContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
        //        return Json(new
        //        {
        //            codehttp = resultadoApi.codeHTTP,
        //            Message = resultadoApi.data.Message
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        Request.RequestContext.HttpContext.Response.StatusCode = (int)200;
        //        return Json(new { codehttp = resultadoApi.codeHTTP, Message = resultadoApi.messageHTTP }, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}
