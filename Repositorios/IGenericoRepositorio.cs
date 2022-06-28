using System.Linq.Expressions;

namespace IconosGeograficos.Repositorios
{
    public interface IGenericoRepositorio<TEntidad> where TEntidad : class
    {
        Task<List<TEntidad>> GetAll();
        Task<TEntidad> GetById(int id); // buscar por id
        Task<TEntidad> Insert(TEntidad entity);
        Task<TEntidad> Update(TEntidad entity);
        Task Delete(int id);
        //Adicionales
        Task<TEntidad> SingleOrDefaultAsync(Expression<Func<TEntidad, bool>> predicate); // to search by name
        Task<TEntidad> FirstOrDefaultAsync(Expression<Func<TEntidad, bool>> predicate); // to search by name
        IQueryable<TEntidad> GetQueryable();
    }
}
