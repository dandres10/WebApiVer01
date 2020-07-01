namespace WebApiVer01.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApiVer01.Context;
    using WebApiVer01.Entitys;
    using WebApiVer01.Helpers;
    using WebApiVer01.Models;
    using WebApiVer01.Services;


    [ApiController]
    [Route("api/[controller]")]

    //[HttpHeaderIsPresent("x-version","1")]
    
    public class AutoresController : ControllerBase
    {
        private readonly ApplicacionDbContext context;
        private readonly IClaseB claseB;
        private readonly ILogger<AutoresController> logger;
        private readonly IMapper mapper;

        public AutoresController(ApplicacionDbContext context, IClaseB claseB, ILogger<AutoresController> logger, IMapper mapper)
        {
            this.context = context;
            this.claseB = claseB;
            this.logger = logger;
            this.mapper = mapper;
        }

        // POST api/autores
       
        //[Route("CrearAutor")]
        [HttpPost(Name = "CrearAutor")]
        public async Task<ActionResult> Post([FromBody] AutorCreacionDTO autorCreacion)
        {
            var autor = mapper.Map<Autor>(autorCreacion);
            context.Add(autor);
            await context.SaveChangesAsync();
            var autorDTO = mapper.Map<AutorDTO>(autor); 
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autorDTO);
        }

        
        //[Route("ObtenerAutores")]
        [ServiceFilter(typeof(HATEOASAuthorFilterAttribute))]
        [HttpGet(Name = "ObtenerAutores")]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> Get(int numeroPagina = 1, int cantidadRegistros = 10)
        {

            var query = context.Autores.AsQueryable();
            var totalRegistros = query.Count();

            var autores = await query
                .Skip(cantidadRegistros * (numeroPagina-1))
                .Take(cantidadRegistros)
                .ToListAsync();

            Response.Headers["X-Total-Registros"] = totalRegistros.ToString();
            Response.Headers["X-Cantidad-Paginas"] = ((int)Math.Ceiling((double)totalRegistros / cantidadRegistros)).ToString();

            var autoresDTO = mapper.Map<List<AutorDTO>>(autores);

            return Ok(autoresDTO);
        }

        //private void GenerarEnlaces(AutorDTO autor)
        //{
        //    autor.Enlaces.Add(new Enlace(Url.Link("ObtenerAutor", new { id = autor.Id }), rel: "self", metodo: "GET"));
        //    autor.Enlaces.Add(new Enlace(Url.Link("ActualizarAutor", new { id = autor.Id }), rel: "update-author", metodo: "PUT"));
        //    autor.Enlaces.Add(new Enlace(Url.Link("BorrarAutor", new { id = autor.Id }), rel: "delete-author", metodo: "DELETE"));
        //}

      
        //[Route("Primero")]
        //[HttpGet(Name = "Primero")]
        //public ActionResult<Autor> GetPrimerAutor()
        //{
        //    return context.Autores.FirstOrDefault();
        //}

        //Trabajando esto
        
        //[Route("ObtenerAutor/{id?}")]
        [ServiceFilter(typeof(HATEOASAuthorFilterAttribute))]
        [HttpGet("{id}", Name = "ObtenerAutor")]
        public async Task<ActionResult<AutorDTO>> Get(int id)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            var autorDTO = mapper.Map<AutorDTO>(autor);

            //GenerarEnlaces(autorDTO);

            return autorDTO;
        }

        [HttpPut("{id}", Name = "ActualizarAutor")]
        public async Task<ActionResult> Put(int id, [FromBody] AutorCreacionDTO autorActualizacion)
        {
            var autor = mapper.Map<Autor>(autorActualizacion);
            autor.Id = id;
            context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}", Name = "ActualizarParcialmenteAutor")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<AutorCreacionDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var autorDeLaDB = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if (autorDeLaDB == null)
            {
                return NotFound();
            }

            var autorDTO = mapper.Map<AutorCreacionDTO>(autorDeLaDB);

            patchDocument.ApplyTo(autorDTO, ModelState);

            mapper.Map(autorDTO, autorDeLaDB);

            var isValid = TryValidateModel(autorDeLaDB);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            await context.SaveChangesAsync();
            return NoContent();
        }
        /// <summary>
        /// Borra un elemento específico
        /// </summary>
        /// <param name="id">Id del elemento a borrar</param>   
        [HttpDelete("{id}", Name = "BorrarAutor")]
        public async Task<ActionResult<Autor>> Delete(int id)
        {
            var autorId = await context.Autores.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (autorId == default(int))
            {
                return NotFound();
            }

            context.Autores.Remove(new Autor { Id = autorId });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}