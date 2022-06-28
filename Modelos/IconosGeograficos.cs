using System.ComponentModel.DataAnnotations;

namespace IconosGeograficos.Modelos
{
    public class IconosGeograficos
    {
        [Key]
        public int? id { get; set; }
        public string urlImagen { get; set; }
        public string denominacion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public double altura { get; set; }
        public string historia { get; set; }

        // Collection navigation property 
        public ICollection<Ciudades> ciudades { get; set; }
    }
}
