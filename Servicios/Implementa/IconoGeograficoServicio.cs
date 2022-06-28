using IconosGeograficos.Repositorios;

namespace IconosGeograficos.Servicios.Implementa
{
    public class IconoGeograficoServicio : GenericoServicio<Modelos.IconosGeograficos>, IiconoGeograficoServicio
    {
        private IiconoGeograficoRepositorio _iconoGeoRepositorio;
        public IconoGeograficoServicio(IiconoGeograficoRepositorio icoRepositorio) : base(icoRepositorio)
        {
            _iconoGeoRepositorio = icoRepositorio;
        }

        public IQueryable<Modelos.IconosGeograficos> GetIcono(string denominacion, DateTime FechaCreacion, double altura, int idCiudad)
        {
           return _iconoGeoRepositorio.GetIcono(denominacion,FechaCreacion,altura,idCiudad);
        }

        public IQueryable<Modelos.IconosGeograficos> GetIconosGeograficosDetalles()
        {
            return _iconoGeoRepositorio.GetIconosGeograficosDetalles();
        }
    }
}
