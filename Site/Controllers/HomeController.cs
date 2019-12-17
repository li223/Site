using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Site.Models;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Linq;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        Config Config { get; set; }

        public HomeController(Config conf) => this.Config = conf;

        public IActionResult Index() => View();

        [Route("noscript")]
        public IActionResult Noscript() => View();

        [Route("upload")]
        public IActionResult NerdsOnly() => View("Views/Shared/Upload.cshtml");

        [Route("upload/action")]
        public async Task<IActionResult> UploadAction()
        {
            this.Request.Form.TryGetValue("type", out var primtype);
            this.Request.Form.TryGetValue("pw", out var primpw);
            var type = primtype[0].ToString();
            var pw = primpw[0].ToString();
            if (pw.ToLower() != this.Config.UploadPassword) return this.Unauthorized();
            var limit = int.Parse(await System.IO.File.ReadAllTextAsync($"/{type}/limit.txt"));
            using var filestream = System.IO.File.Create($"/punch/{limit += 1}.gif");
            await this.Request.Form.Files[0].OpenReadStream().CopyToAsync(filestream);
            await System.IO.File.WriteAllTextAsync($"/{type}/limit.txt", (limit += 1).ToString());
            return Index();
        }

        [Route("docs/chandler")]
        public IActionResult ChandlerDocs() => View("Views/CHANdler/Docs.cshtml");

        [Route("docs/chandler/objects")]
        public IActionResult ChandlerObjectDocs() => View("Views/CHANdler/Objects.cshtml");

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorCode = this.HttpContext.Response.StatusCode });
    }
}
