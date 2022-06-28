using System.ComponentModel.DataAnnotations;

namespace IconosGeograficos.VistasModelos.Auth.Registro
{
    public class ModeloRequestRegistro
    {
        [Required]
        [MinLength(6)]
        public string Usuario { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
