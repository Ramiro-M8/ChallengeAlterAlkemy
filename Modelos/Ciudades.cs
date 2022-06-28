using System.ComponentModel.DataAnnotations;

namespace IconosGeograficos.Modelos
{
    public class Ciudades
    {
        [Key]
        public int? id { get; set; }
        public string urlImagen { get; set; }
        public string denominacion { get; set; }
        public int cantidadHabitantes { get; set; }
        public double superficieTotal { get; set; }
        
        public int continenteId { get; set; }
        public Continente continente { get; set; }
        public ICollection<IconosGeograficos> iconosGeograficos { get; set; }
    }
}
