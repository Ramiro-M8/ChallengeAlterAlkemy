using System.ComponentModel.DataAnnotations;

namespace IconosGeograficos.VistasModelos.Auth.Login
{
    public class ModeloRequestLogin
    {
        [Required]
        [MinLength(6)]
        public string Usuario { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
