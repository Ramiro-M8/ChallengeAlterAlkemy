using IconosGeograficos.Data;
using IconosGeograficos.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace IconosGeograficos.Repositorios.Implementa
{
    public class CiudadesRepositorio : GenericoRepositorio<Ciudades>, ICiudadesRepositorio
    {
        private readonly GeoContext _dbContext;
        public CiudadesRepositorio(GeoContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public IQueryable<Ciudades> GetCiudad(string denominacion, int idContinente, string orderBy)
        {
            var order = orderBy == "asc" ? "ascending" : "descending";
            var query = _dbContext.ciudades
                        .Include(i => i.continente)
                        .Include(i=> i.iconosGeograficos)
                        .Where(i => i.denominacion.Contains(denominacion) || i.continenteId == idContinente)
                        .OrderBy($"id {order}")
                        .Select(i => new Modelos.Ciudades
                        {
                            urlImagen = i.urlImagen,
                            denominacion = i.denominacion,
                            cantidadHabitantes = i.cantidadHabitantes,
                            superficieTotal = i.superficieTotal,
                            iconosGeograficos = i.iconosGeograficos,
                            continente = i.continente
                        });
            return query;
        }

        public IQueryable<Ciudades> GetDetalleCiudad()
        {
            return _dbContext.ciudades.Include(i => i.iconosGeograficos).Include(i=> i.continente);
        }
    }
}
