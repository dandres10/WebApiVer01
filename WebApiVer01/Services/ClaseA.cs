namespace WebApiVer01.Services
{
    public class ClaseA
    {
        private readonly IClaseB claseB;

        public ClaseA(IClaseB claseB)
        {
            this.claseB = claseB;
        }

        public void RealizarAccion()
        {
            claseB.HacerAlgo();
        }
    }
}