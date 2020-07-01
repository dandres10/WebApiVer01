namespace WebApiVer01.Services
{
    using Microsoft.Extensions.Logging;

    public class ClaseB : IClaseB
    {
        private readonly ILogger<ClaseB> logger;

        public ClaseB(ILogger<ClaseB> logger)
        {
            this.logger = logger;
        }

        public void HacerAlgo()
        {
            logger.LogInformation("Ejecutamos el metodo HacerAlgo de la claseB");
        }
    }

    public class ClaseB2 : IClaseB
    {
        public void HacerAlgo()
        {
        }
    }
}