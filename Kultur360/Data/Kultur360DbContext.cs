using Kultur360.Models;
using Microsoft.EntityFrameworkCore;

namespace Kultur360.Data
{
    public class Kultur360DbContext : DbContext
    {
        public Kultur360DbContext(DbContextOptions<Kultur360DbContext> options) : base(options)
        {
        }

        public DbSet<TarihiYer> TarihiYerler { get; set; }
    }
}
