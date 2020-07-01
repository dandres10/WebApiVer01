namespace WebApiVer01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AutorDTO: Recurso
    {
        public int Id { get; set; }

    
        public string Nombre { get; set; }

        public DateTime FechaNacimiento { get; set; }
        public List<LibroDTO> Books { get; set; }
    }
}