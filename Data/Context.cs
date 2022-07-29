using Microsoft.EntityFrameworkCore;
using MoeBinAPI.Models;

namespace MoeBinAPI.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opt) : base(opt)
        {
            
        }

        public DbSet<Paste> Pastes { get; set; } = null!;
    }
}