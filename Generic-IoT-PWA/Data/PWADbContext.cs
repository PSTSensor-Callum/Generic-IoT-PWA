using Generic_IoT_PWA.Models.GenericSensor;
using Microsoft.EntityFrameworkCore;

namespace Generic_IoT_PWA.Data
{
    public class PWADbContext : DbContext
    {
        public DbSet<GenericSensor> GenericSensors { get; set; }

        public PWADbContext(DbContextOptions<PWADbContext> options) : base(options) { }
    }
}
