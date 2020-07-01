namespace WebApiVer01.Context
{
    using Microsoft.EntityFrameworkCore;
    using WebApiVer01.Entitys;

    public class ApplicacionDbContext : DbContext
    {
        public ApplicacionDbContext(DbContextOptions<ApplicacionDbContext> options): base(options)
        {

        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}