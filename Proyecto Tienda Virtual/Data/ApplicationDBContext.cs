using Microsoft.EntityFrameworkCore;
using Proyecto_Tienda_Virtual.ModelsFromDb;

namespace Proyecto_Tienda_Virtual.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
    }
}
