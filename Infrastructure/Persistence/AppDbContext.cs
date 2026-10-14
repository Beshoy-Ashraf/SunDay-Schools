using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> option) : DbContext(option)
{
      public DbSet<User> Users { get; set; }
      public DbSet<RefreshToken> RefreshTokens { get; set; }
      public DbSet<Eparchy> Eparchies { get; set; }
      public DbSet<Church> Churches { get; set; }
      public DbSet<ChurchMember> ChurchMembers { get; set; }
      public DbSet<ChurchService> ChurchServices { get; set; }
      public DbSet<ChurchServiceSession> ChurchServiceSessions { get; set; }
      public DbSet<ChurchServiceMember> ChurchServiceMembers { get; set; }
      public DbSet<ActivityType> ActivityTypes { get; set; }
      public DbSet<ChurchServiceAttendance> ChurchServiceAttendances { get; set; }
      public DbSet<ServantActivityRecord> ServantActivityRecords { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
      }
}
