using IconosGeograficos.Modelos;
using IconosGeograficos.Repositorios;

namespace IconosGeograficos.Servicios.Implementa
{
    public class CiudadesServicio : GenericoServicio<Ciudades>, ICiudadesServicio
    {
        private ICiudadesRepositorio _ciudadesRepositorio;
        public CiudadesServicio(ICiudadesRepositorio ciudadesRepositorio) : base(ciudadesRepositorio)
        {
            _ciudadesRepositorio = ciudadesRepositorio;
        }

        public IQueryable<Ciudades> GetCiudades(string denominacion, int idContinente, string orderBy)
        {
            return _ciudadesRepositorio.GetCiudad(denominacion, idContinente, orderBy);
        }

        public IQueryable<Ciudades> GetDetalleCiudades()
        {
            return _ciudadesRepositorio.GetDetalleCiudad();
        }
    }
}
