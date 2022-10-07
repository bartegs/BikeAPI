using Microsoft.EntityFrameworkCore;

namespace BikeAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Bike> Bikes { get; set; }
    }
}
