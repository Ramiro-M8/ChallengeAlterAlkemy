using IconosGeograficos.Data;
using IconosGeograficos.Modelos;
using Microsoft.EntityFrameworkCore;

namespace IconosGeograficos.Repositorios.Implementa
{
    public class IconoGeograficoRepositorio : GenericoRepositorio<Modelos.IconosGeograficos>, IiconoGeograficoRepositorio
    {
        private readonly GeoContext _dbContext;
        public IconoGeograficoRepositorio(GeoContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public IQueryable<Modelos.IconosGeograficos> GetIcono(string denominacion, DateTime FechaCreacion, double altura, int idCiudad)
        {
            var query = _dbContext.iconosGeograficos
                        .Include(i => i.ciudades)
                        .ThenInclude(c => c.continente)
                        .Where(i => i.denominacion.Contains(denominacion) || i.fechaCreacion == FechaCreacion || i.altura == altura ||
                        i.ciudades.Any(x => x.id == idCiudad))
                        .Select(i => new Modelos.IconosGeograficos
                        {
                            urlImagen = i.urlImagen,
                            denominacion = i.denominacion,
                            fechaCreacion = i.fechaCreacion,
                            altura = i.altura,
                            historia = i.historia,
                            ciudades = i.ciudades                           
                        });
            return query;
        }

        public IQueryable<Modelos.IconosGeograficos> GetIconosGeograficosDetalles()
        {
            return _dbContext.iconosGeograficos.Include(c => c.ciudades).ThenInclude(c => c.continente);
        }
    }
}
