using Microsoft.EntityFrameworkCore;
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

            users.HasOne(x => x.UserType).WithMany(x => x.User).HasForeignKey(x => x.FKIdUserType).OnDelete(DeleteBehavior.NoAction);



            var permissions = modelBuilder.Entity<Permissions>();
            permissions.Property(x => x.IdPermission);
            permissions.Property(x => x.Permission).IsRequired().HasMaxLength(50);

            var usertypes = modelBuilder.Entity<UserType>();
            usertypes.Property(x => x.IdUserType);
            usertypes.Property(x => x.UserTypeName).IsRequired().HasMaxLength(50);



            var permissionXusertypes = modelBuilder.Entity<PermissionsXUserTypes>();
            permissionXusertypes.Property(x => x.IdPermissionsXUserTypes);

            permissionXusertypes.HasOne(x => x.UserType).WithMany(x => x.PermissionsXUserType).HasForeignKey(x => x.FKIdUserType).OnDelete(DeleteBehavior.NoAction);
            permissionXusertypes.HasOne(x => x.Permissions).WithMany(x => x.PermissionsXUserType).HasForeignKey(x => x.FKIdPermission).OnDelete(DeleteBehavior.NoAction);



            var assignment = modelBuilder.Entity<Assignment>();
            assignment.Property(x => x.AssignmentID);
            assignment.Property(x => x.AssignmentDate).IsRequired();

            assignment.HasOne(x => x.Bus).WithMany(x => x.Assignment).HasForeignKey(x => x.FkBusID).OnDelete(DeleteBehavior.NoAction);
            assignment.HasOne(x => x.Route).WithMany(x => x.Assignment).HasForeignKey(x => x.FkRouteID).OnDelete(DeleteBehavior.NoAction);

            var bus = modelBuilder.Entity<Bus>();
            bus.Property(x => x.IdBus);
            bus.Property(x => x.LicensePlate).IsRequired().HasMaxLength(50);
            bus.Property(x => x.Capacity).IsRequired().HasMaxLength(50);
            bus.Property(x => x.Status).IsRequired();

            var notification = modelBuilder.Entity<Notification>();
            notification.Property(x => x.IDNotification);
            notification.Property(x => x.Message).IsRequired().HasMaxLength(200);
            notification.Property(x => x.DateTime).IsRequired();

            notification.HasOne(x => x.Schedule).WithMany(x => x.Notifications).HasForeignKey(x => x.FkIdSchedule).OnDelete(DeleteBehavior.NoAction);


            var route = modelBuilder.Entity<Modelo.Route>();
            route.Property(x => x.IdRoute);
            route.Property(x => x.RouteName).IsRequired().HasMaxLength(50);
            route.Property(x => x.Origin).IsRequired().HasMaxLength(50);
            route.Property(x => x.Destination).IsRequired().HasMaxLength(50);
            route.Property(x => x.EstimatedDuration).IsRequired();


            var schedule = modelBuilder.Entity<Schedule>();
            schedule.Property(x => x.IDSchedule);
            schedule.Property(x => x.DepartureTime).IsRequired();
            schedule.Property(x => x.ArrivalTime).IsRequired();
            schedule.Property(x => x.Status).IsRequired();

            schedule.HasOne(x => x.Route).WithMany(x => x.Schedules).HasForeignKey(x => x.FkIDRoute).OnDelete(DeleteBehavior.Cascade);

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Bus> Bus { get; set; }
        public DbSet<Modelo.Route> Route { get; set; }




    
    }
}
