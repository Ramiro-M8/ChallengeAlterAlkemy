namespace IconosGeograficos.VistasModelos.CRUD.IconosGeograficos
{
    public class ActualizarIconoGeograficoDTO
    {
        public int id { get; set; }
        public string urlImagen { get; set; }
        public string denominacion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public double altura { get; set; }
        public string historia { get; set; }
    }
}
