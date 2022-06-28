using IconosGeograficos.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IconosGeograficos.Data
{
    public class UsuContext : IdentityDbContext<Usuario>
    {
        private const string schema = "users";

        public UsuContext(DbContextOptions<UsuContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(schema);
        }
    }
}
