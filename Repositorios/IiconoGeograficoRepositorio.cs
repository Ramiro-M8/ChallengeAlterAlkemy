namespace IconosGeograficos.Repositorios
{
    public interface IiconoGeograficoRepositorio : IGenericoRepositorio<Modelos.IconosGeograficos>
    {
        IQueryable<Modelos.IconosGeograficos> GetIconosGeograficosDetalles();
        IQueryable<Modelos.IconosGeograficos> GetIcono(string denominacion, DateTime FechaCreacion, double altura, int idCiudad);
    }
}
