using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

public static class SystemAdminSeeder
{
      /// <summary>
      /// Promotes the registered users listed under "SystemAdmins" (emails) to system admin.
      /// This is how the first admin is created; register the account first, then restart.
      /// </summary>
      public static async Task SeedSystemAdminsAsync(this IServiceProvider services, IConfiguration configuration)
      {
            var emails = configuration.GetSection("SystemAdmins").GetChildren()
                  .Select(c => c.Value?.Trim().ToLowerInvariant())
                  .OfType<string>()
                  .Where(e => e.Length > 0)
                  .ToArray();
            if (emails.Length == 0)
                  return;

            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(SystemAdminSeeder));

            var users = await db.Users.Where(u => emails.Contains(u.Email)).ToListAsync();
            foreach (var user in users.Where(u => u.Role != SystemRole.Admin))
            {
                  user.ChangeSystemRole(SystemRole.Admin);
                  logger.LogInformation("Promoted {Email} to system admin", user.Email);
            }

            foreach (var missing in emails.Except(users.Select(u => u.Email)))
                  logger.LogWarning("SystemAdmins lists {Email}, but no user with that email is registered yet", missing);

            await db.SaveChangesAsync();
      }
}
