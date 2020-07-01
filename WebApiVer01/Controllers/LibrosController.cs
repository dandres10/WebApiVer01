namespace WebApiVer01.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using WebApiVer01.Context;
    using WebApiVer01.Entitys;

    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicacionDbContext context;

        public LibrosController(ApplicacionDbContext context)
        {
            this.context = context;
        }

        [HttpGet(Name = "ObtenerLibros")]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            return context.Libros.ToList();
        }

        [HttpGet("{id}", Name = "ObtenerLibro")]
        public ActionResult<Libro> Get(int id)
        {
            var libro = context.Libros.FirstOrDefault(x => x.Id == id);
            if (libro == null)
            {
                return NotFound();
            }
            return libro;
        }

        [HttpPost(Name = "CrearLibro")]
        public ActionResult Post([FromBody] Libro libro)
        {
            context.Libros.Add(libro);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerLibro", new { id = libro.Id }, libro);
        }

        [HttpPut("{id}", Name ="ActualizarLibro")]
        public ActionResult Put(int id, [FromBody] Libro value)
        {

            if (id != value.Id)
            {
                return BadRequest();
            }

            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}", Name ="EliminarLibro")]
        public ActionResult<Libro> Delete(int id)
        {

            var libro = context.Libros.FirstOrDefault(x => x.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            context.Libros.Remove(libro);
            context.SaveChanges();
            return libro;

        }
    }
}