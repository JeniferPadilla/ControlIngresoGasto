using ControlIngresoGasto.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlIngresoGasto.datas
{
    public class AplicationDbContext :DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext>options):base(options)
        {
                
        }

        public DbSet<Categoria> Categorias{ get; set; }

        public DbSet<IngrseoGasto> IngresoGasto { get; set; } //con este modelo se genera
    }
}
