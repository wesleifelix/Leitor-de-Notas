using LeitorDeNotas.ClearArch.Application.Interfaces;
using LeitorDeNotas.ClearArch.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LeitorDeNotas.ClearArch.Api.Controllers
{
    public class BaseController : Controller
    {
        protected ActionResult HandleException(Exception ex)
        {

            string mensagemErro = (ex.Message);

            if (ex is Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, mensagemErro);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, mensagemErro);
            }
        }

    }
}
