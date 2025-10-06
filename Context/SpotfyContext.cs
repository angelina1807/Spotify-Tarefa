using Microsoft.EntityFrameworkCore;
using TarefaSpotfyApi.Models;

namespace TarefaSpotfyApi.Context
{
    public class SpotfyContext : DbContext
    {
        public SpotfyContext(DbContextOptions<SpotfyContext> options)
            : base(options)
        {
        }

        public DbSet<Spotfy> Spotfys { get; set; } = null!;
    }
}
