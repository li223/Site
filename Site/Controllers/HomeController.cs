using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Site.Models;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        [Route("noscript")]
        public IActionResult Noscript() => View();

        [Route("docs/chandler")]
        public IActionResult ChandlerDocs() => View("Views/CHANdler/Docs.cshtml");

        [Route("docs/chandler/objects")]
        public IActionResult ChandlerObjectDocs() => View("Views/CHANdler/Objects.cshtml");

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorCode = this.HttpContext.Response.StatusCode });
    }
}
