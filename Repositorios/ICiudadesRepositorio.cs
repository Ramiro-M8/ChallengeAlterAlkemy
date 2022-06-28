namespace IconosGeograficos.Repositorios
{
    public interface ICiudadesRepositorio : IGenericoRepositorio<Modelos.Ciudades>
    {
        IQueryable<Modelos.Ciudades> GetDetalleCiudad();
        IQueryable<Modelos.Ciudades> GetCiudad(string denominacion, int idContinente, string orderBy);
    }
}
