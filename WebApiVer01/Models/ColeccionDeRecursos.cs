namespace WebApiVer01.Models
{
    using System.Collections.Generic;

    public class ColeccionDeRecursos<T> : Recurso where T : Recurso
    {
        public List<T> Valores { get; set; }

        public ColeccionDeRecursos(List<T> valores)
        {
            Valores = valores;
        }
    }
}