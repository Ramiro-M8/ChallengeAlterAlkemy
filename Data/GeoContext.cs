using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IconosGeograficos.Modelos;
namespace IconosGeograficos.Data
{
    public class GeoContext : DbContext
    {
        public GeoContext(DbContextOptions<GeoContext> options) : base(options)
        {

        }

        public DbSet<Ciudades> ciudades { get; set; }
        public DbSet<Continente> continentes { get; set; }
        public DbSet<Modelos.IconosGeograficos> iconosGeograficos { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("APIGeoConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
