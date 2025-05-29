using ApplicationTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Application> Applications => Set<Application>();
        
        public DbSet<Status> Statuses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Application>()
                .Property(app =>app.Status)
                .HasConversion<int>();
            
            modelBuilder.Entity<Status>()
                .HasData(new List<Status>()
                {
                    new Status() { Id = 1, Name = "Interview" },
                    new Status() { Id = 2, Name = "Offer" },
                    new Status() { Id = 3, Name = "Rejected" },
                });
            
            modelBuilder.Entity<Application>()
                .HasData(new List<Application>()
                {
                    new Application() { Id = 1, CompanyName = "Company A", DateApplied = new DateOnly(2025, 05, 20), Position = "Full Stack .NET Developer", Status = ApplicationStatus.Interview },
                    new Application() { Id = 2, CompanyName = "Company B", DateApplied = new DateOnly(2025, 05, 21), Position = "Full Stack .NET Developer", Status = ApplicationStatus.Offer },
                    new Application() { Id = 3, CompanyName = "Company C", DateApplied = new DateOnly(2025, 05, 22), Position = "Full Stack .NET Developer", Status = ApplicationStatus.Rejected }
                });
        }
    }
}
