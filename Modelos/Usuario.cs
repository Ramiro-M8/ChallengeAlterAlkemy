using Microsoft.AspNetCore.Identity;

namespace IconosGeograficos.Modelos
{
    public class Usuario : IdentityUser
    {
        public bool isActive { get; set; } //Control de baja logica
    }
}
