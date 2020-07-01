namespace WebApiVer01.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using WebApiVer01.Models;

    [ApiController]
    [Route("api")]
    public class RootController: ControllerBase
    {
        



       
        //[Route("GetRoot")]
        [HttpGet(Name = "GetRoot")]
        public ActionResult<IEnumerable<Enlace>> Get()
        {
            List<Enlace> enlaces = new List<Enlace>();

            // Aquí colocamos los links
            enlaces.Add(new Enlace(href: Url.Link("GetRoot", new { }), rel: "self", metodo: "GET"));
            enlaces.Add(new Enlace(href: Url.Link("ObtenerAutores", new { }), rel: "autores", metodo: "GET"));
            enlaces.Add(new Enlace(href: Url.Link("CrearAutor", new { }), rel: "crear-autor", metodo: "POST"));
            enlaces.Add(new Enlace(href: Url.Link("ObtenerValores", new { }), rel: "valores", metodo: "GET"));
            enlaces.Add(new Enlace(href: Url.Link("CrearValor", new { }), rel: "crear-valor", metodo: "POST"));

            return enlaces;
        }
    }
}