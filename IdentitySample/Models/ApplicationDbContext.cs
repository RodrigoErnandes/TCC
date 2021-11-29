using System.Data.Entity;
using System.Security.Claims;
using IdentitySample.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentitySample.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Client> Client { get; set; }

        public DbSet<Claims> Claims { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<IdentitySample.Models.Livro> Livros { get; set; }

        public System.Data.Entity.DbSet<IdentitySample.Models.Emprestimo> Emprestimos { get; set; }
        public System.Data.Entity.DbSet<IdentitySample.Models.Acervo> Acervos { get; set; }
        public System.Data.Entity.DbSet<IdentitySample.Models.Baixa> Baixas { get; set; }
        public System.Data.Entity.DbSet<IdentitySample.Models.Reserva> Reservas { get; set; }
        public System.Data.Entity.DbSet<IdentitySample.Models.ReservaLivro> ReservaLivros { get; set; }
        public System.Data.Entity.DbSet<IdentitySample.Models.Multa> Multas { get; set; }
    }
}