using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestHealtStrategies.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult UrlNotFound()
        {
            ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found. Status code: 404";
            //var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            //ViewBag.Path = statusCodeResult.OriginalPath;
            //ViewBag.QueryString = statusCodeResult.OriginalQueryString;
            return View("NotFound");
        }
    }
}
