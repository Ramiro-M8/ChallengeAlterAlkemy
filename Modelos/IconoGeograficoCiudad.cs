namespace IconosGeograficos.Modelos
{
    public class IconoGeograficoCiudad
    {
        public int IconoGeograficoID { get; set; }
        public IconosGeograficos IconoGeografico { get; set; }

        public int CiudadID { get; set; }
        public Ciudades Ciudad { get; set; }
    }
}
