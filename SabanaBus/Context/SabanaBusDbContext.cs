using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SabanaBus.Modelo;
using System.Net.WebSockets;
namespace SabanaBus.Context
{
    public class SabanaBusDbContext : DbContext
    {
        public SabanaBusDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var users = modelBuilder.Entity<User>();
            users.Property(x => x.IdUser);
            users.Property(x => x.Name).IsRequired().HasMaxLength(50);
            users.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            users.Property(x => x.Email).IsRequired().HasMaxLength(50);
            users.Property(x => x.Password).IsRequired().HasMaxLength(50);
            users.Property(x => x.DateCreated);
            users.Property(x => x.ModifiedDate);
            users.Property(x => x.ModifiedBy);
            users.Property(x => x.IsDeleted).IsRequired();

            users.HasOne(x => x.UserType).WithMany(x => x.User).HasForeignKey(x => x.FKIdUserType).OnDelete(DeleteBehavior.NoAction);



            var permissions = modelBuilder.Entity<Permissions>();
            permissions.Property(x => x.IdPermission);
            permissions.Property(x => x.Permission).IsRequired().HasMaxLength(50);
            permissions.Property(x => x.IsDeleted).IsRequired();

            var usertypes = modelBuilder.Entity<UserType>();
            usertypes.Property(x => x.IdUserType);
            usertypes.Property(x => x.UserTypeName).IsRequired().HasMaxLength(50);
            usertypes.Property(x => x.IsDeleted).IsRequired();



            var permissionXusertypes = modelBuilder.Entity<PermissionsXUserTypes>();
            permissionXusertypes.Property(x => x.IdPermissionsXUserTypes);
            permissionXusertypes.Property(x => x.IsDeleted).IsRequired();

            permissionXusertypes.HasOne(x => x.UserType).WithMany(x => x.PermissionsXUserType).HasForeignKey(x => x.FKIdUserType).OnDelete(DeleteBehavior.NoAction);
            permissionXusertypes.HasOne(x => x.Permissions).WithMany(x => x.PermissionsXUserType).HasForeignKey(x => x.FKIdPermission).OnDelete(DeleteBehavior.NoAction);



            var assignment = modelBuilder.Entity<Assignment>();
            assignment.Property(x => x.AssignmentID);
            assignment.Property(x => x.AssignmentDate).IsRequired();
            assignment.Property(x => x.IsDeleted).IsRequired();


            assignment.HasOne(x => x.Bus).WithMany(x => x.Assignment).HasForeignKey(x => x.FkBusID).OnDelete(DeleteBehavior.NoAction);
            assignment.HasOne(x => x.Route).WithMany(x => x.Assignment).HasForeignKey(x => x.FkRouteID).OnDelete(DeleteBehavior.NoAction);

            var bus = modelBuilder.Entity<Bus>();
            bus.Property(x => x.IdBus);
            bus.Property(x => x.LicensePlate).IsRequired().HasMaxLength(50);
            bus.Property(x => x.Capacity).IsRequired().HasMaxLength(50);
            bus.Property(x => x.Status).IsRequired();
            bus.Property(x => x.IsDeleted).IsRequired();

            var notification = modelBuilder.Entity<Notification>();
            notification.Property(x => x.IDNotification);
            notification.Property(x => x.Message).IsRequired().HasMaxLength(200);
            notification.Property(x => x.DateTime).IsRequired();
            notification.Property(x => x.IsDeleted).IsRequired();

            notification.HasOne(x => x.Schedule).WithMany(x => x.Notifications).HasForeignKey(x => x.FkIdSchedule).OnDelete(DeleteBehavior.NoAction);


            var route = modelBuilder.Entity<Modelo.Route>();
            route.Property(x => x.IdRoute);
            route.Property(x => x.RouteName).IsRequired().HasMaxLength(50);
            route.Property(x => x.Origin).IsRequired().HasMaxLength(50);
            route.Property(x => x.Destination).IsRequired().HasMaxLength(50);
            route.Property(x => x.EstimatedDuration).IsRequired();
            route.Property(x => x.IsDeleted).IsRequired();


            var schedule = modelBuilder.Entity<Schedule>();
            schedule.Property(x => x.IDSchedule);
            schedule.Property(x => x.DepartureTime).IsRequired();
            schedule.Property(x => x.ArrivalTime).IsRequired();
            schedule.Property(x => x.Status).IsRequired();
            schedule.Property(x => x.IsDeleted).IsRequired();

            schedule.HasOne(x => x.Route).WithMany(x => x.Schedules).HasForeignKey(x => x.FkIDRoute).OnDelete(DeleteBehavior.NoAction);

            var auditlog = modelBuilder.Entity<AuditLog>();
            auditlog.HasKey(x => x.Id);
            auditlog.Property(x => x.EntityName).HasColumnType("varchar(64)").IsRequired();
            auditlog.Property(x => x.EntityId).IsRequired();
            auditlog.Property(x => x.ActionType).HasColumnType("varchar(64)").IsRequired();
            auditlog.Property(x => x.OldValues).HasColumnType("varchar(MAX)").IsRequired(false);
            auditlog.Property(x => x.NewValues).HasColumnType("varchar(MAX)").IsRequired();
            auditlog.Property(x => x.Date).IsRequired();
            auditlog.Property(x => x.ModifiedBy).HasColumnType("varchar(64)").IsRequired();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            // Capturar la información del usuario actual y IP (esto puede venir del contexto HTTP o de la autenticación)
            string currentUser = "system"; // Reemplazar con el usuario actual del sistema

            // Audit changes
            var auditEntries = OnBeforeSaveChanges(currentUser);

            // Guardar los cambios en la base de datos
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            // Si hay auditorías, se guardan luego de realizar SaveChangesAsync
            if (auditEntries.Any())
            {
                await AuditLogs.AddRangeAsync(auditEntries);
                await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }

            return result;
        }

        private List<AuditLog> OnBeforeSaveChanges(string user)
        {
            var auditEntries = new List<AuditLog>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditLog = new AuditLog
                {
                    EntityName = entry.Entity.GetType().Name,
                    Date = DateTime.UtcNow,
                    ModifiedBy = user,
                };

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditLog.ActionType = "Create";
                        auditLog.NewValues = JsonConvert.SerializeObject(entry.CurrentValues.ToObject());
                        break;

                    case EntityState.Deleted:
                        auditLog.ActionType = "Delete";
                        auditLog.OldValues = JsonConvert.SerializeObject(entry.OriginalValues.ToObject());
                        break;

                    case EntityState.Modified:
                        auditLog.ActionType = "Update";
                        auditLog.OldValues = JsonConvert.SerializeObject(entry.OriginalValues.ToObject());
                        auditLog.NewValues = JsonConvert.SerializeObject(entry.CurrentValues.ToObject());
                        break;
                }

                // Si la entidad tiene una clave primaria, la capturamos
                var key = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey());
                if (key != null)
                {
                    auditLog.EntityId = (int)key.CurrentValue;
                }

                auditEntries.Add(auditLog);
            }

            return auditEntries;
        }




        // DbSet para AuditLog
        public DbSet<AuditLog> AuditLogs { get; set; }

        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<Bus> Bus { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<PermissionsXUserTypes> PermissionsXUserTypes { get; set; }
        public DbSet<Modelo.Route> Routes { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserType { get; set; }






    }
}
