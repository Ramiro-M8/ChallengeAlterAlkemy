using IconosGeograficos.Repositorios;
using System.Linq.Expressions;

namespace IconosGeograficos.Servicios.Implementa
{
    public class GenericoServicio<TEntidad> : IGenericoServicio<TEntidad> where TEntidad : class
    {
        private IGenericoRepositorio<TEntidad> _genericoRepositorio;

        public GenericoServicio(IGenericoRepositorio<TEntidad> genericoRepositorio)
        {
            this._genericoRepositorio = genericoRepositorio;
        }
    
        public async Task Delete(int id)
        {
            await _genericoRepositorio.Delete(id);
        }

        public async Task<List<TEntidad>> GetAll()
        {
            return await _genericoRepositorio.GetAll();
        }

        public async Task<TEntidad> GetById(int id)
        {
            return await _genericoRepositorio.GetById(id);
        }

        public async Task<TEntidad> Insert(TEntidad entidad)
        {
            return await _genericoRepositorio.Insert(entidad);
        }

        public async Task<TEntidad> Update(TEntidad entidad)
        {
            return await _genericoRepositorio.Update(entidad);
        }
        public Task<TEntidad> SingleOrDefaultAsync(Expression<Func<TEntidad, bool>> predicate)
        {
            return _genericoRepositorio.SingleOrDefaultAsync(predicate);
        }
        public Task<TEntidad> FirstOrDefaultAsync(Expression<Func<TEntidad, bool>> predicate)
        {
            return _genericoRepositorio.FirstOrDefaultAsync(predicate);
        }
        public IQueryable<TEntidad> GetQueryable()
        {
            return _genericoRepositorio.GetQueryable();
        }
    }
}
