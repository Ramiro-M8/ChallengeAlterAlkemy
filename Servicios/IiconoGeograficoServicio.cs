namespace IconosGeograficos.Servicios
{
    public interface IiconoGeograficoServicio : IGenericoServicio<Modelos.IconosGeograficos>
    {
        IQueryable<Modelos.IconosGeograficos> GetIconosGeograficosDetalles();
        IQueryable<Modelos.IconosGeograficos> GetIcono(string denominacion, DateTime FechaCreacion, double altura, int idCiudad);
    }
}
