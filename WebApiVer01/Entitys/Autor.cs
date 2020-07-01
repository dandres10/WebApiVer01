namespace WebApiVer01.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    

    public class Autor
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public List<Libro> Books { get; set; }


    }
}