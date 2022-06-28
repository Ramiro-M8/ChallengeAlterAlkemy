using IconosGeograficos.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IconosGeograficos.Repositorios.Implementa
{
    public class GenericoRepositorio<TEntidad> : IGenericoRepositorio<TEntidad> where TEntidad : class
    {
        private readonly GeoContext geoContext;

        //Constructor
        public GenericoRepositorio(GeoContext dbContext)
        {
            this.geoContext = dbContext;
        }

        public async Task Delete(int id)
        {
            var entidad = await GetById(id);

            if (entidad == null) //Si la entidad que recibe es nula, arroja una excepcion, avisando de que la entidad que recibio es null
                throw new Exception("La entidad recibida es nula");

            geoContext.Set<TEntidad>().Remove(entidad);
            await geoContext.SaveChangesAsync();
        }

        public async Task<List<TEntidad>> GetAll()
        {
            return await geoContext.Set<TEntidad>().ToListAsync();
        }

        public async Task<TEntidad> GetById(int id)
        {
            if (id == null) throw new Exception("El id recibido es nulo");
            return await geoContext.Set<TEntidad>().FindAsync(id);
        }

        public async Task<TEntidad> Insert(TEntidad entidad)
        {
            geoContext.Set<TEntidad>().AddAsync(entidad);
            await geoContext.SaveChangesAsync();
            return entidad;
        }

        public async Task<TEntidad> Update(TEntidad entidad)
        {
            geoContext.Entry(entidad).State = EntityState.Modified;
            await geoContext.SaveChangesAsync();
            return entidad;
        }

        public Task<TEntidad> SingleOrDefaultAsync(Expression<Func<TEntidad, bool>> predicate)
        {
            return geoContext.Set<TEntidad>().SingleOrDefaultAsync(predicate);
        } 

        public Task<TEntidad> FirstOrDefaultAsync(Expression<Func<TEntidad, bool>> predicate)
        {
            return geoContext.Set<TEntidad>().FirstOrDefaultAsync(predicate);
        } 

        public IQueryable<TEntidad> GetQueryable()
        {
            return geoContext.Set<TEntidad>();
        }
    }
}
