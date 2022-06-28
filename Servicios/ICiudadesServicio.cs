using IconosGeograficos.Modelos;

namespace IconosGeograficos.Servicios
{
    public interface ICiudadesServicio : IGenericoServicio<Ciudades>
    {
        IQueryable<Ciudades> GetDetalleCiudades();
        IQueryable<Ciudades> GetCiudades(string denominacion, int idContinente, string orderBy);
    }
}
