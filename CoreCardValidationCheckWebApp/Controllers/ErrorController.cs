using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCardValidationCheckWebApp.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            // Retrieve the exception Details
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")) + exceptionHandlerPathFeature.Error.StackTrace;
            message += Environment.NewLine;
            message += "==========================================================================================================";
            message += Environment.NewLine;           
            using (StreamWriter writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/", "ErrorLog.txt"), true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
            return View("Error");
        }
    }
}
