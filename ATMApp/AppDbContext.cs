using ATMApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ATMApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<AtmDataModel> AtmData { get; set; }
    }
}
