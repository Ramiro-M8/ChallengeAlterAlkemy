using System.ComponentModel.DataAnnotations;

namespace IconosGeograficos.Modelos
{
    public class Continente
    {
        [Key]
        public int? id { get; set; }
        public string urlImagen { get; set; }
        public string denominacion { get; set; }    
        public ICollection<Ciudades> ciudades { get; set; }
    }
}
