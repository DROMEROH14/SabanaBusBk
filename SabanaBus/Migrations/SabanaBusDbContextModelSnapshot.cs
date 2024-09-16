﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SabanaBus.Context;

#nullable disable

namespace SabanaBus.Migrations
{
    [DbContext(typeof(SabanaBusDbContext))]
    partial class SabanaBusDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SabanaBus.Modelo.Assignment", b =>
                {
                    b.Property<int>("AssignmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AssignmentID"));

                    b.Property<DateTime>("AssignmentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FkBusID")
                        .HasColumnType("int");

                    b.Property<int>("FkRouteID")
                        .HasColumnType("int");

                    b.HasKey("AssignmentID");

                    b.HasIndex("FkBusID");

                    b.HasIndex("FkRouteID");

                    b.ToTable("Assignment");
                });

            modelBuilder.Entity("SabanaBus.Modelo.Bus", b =>
                {
                    b.Property<int>("IdBus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdBus"));

                    b.Property<int>("Capacity")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("IdBus");

                    b.ToTable("Bus");
                });

            modelBuilder.Entity("SabanaBus.Modelo.Notification", b =>
                {
                    b.Property<int>("IDNotification")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDNotification"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("FkIdSchedule")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("IDNotification");

                    b.HasIndex("FkIdSchedule");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("SabanaBus.Modelo.Permissions", b =>
                {
                    b.Property<int>("IdPermission")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPermission"));

                    b.Property<string>("Permission")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdPermission");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("SabanaBus.Modelo.PermissionsXUserTypes", b =>
                {
                    b.Property<int>("IdPermissionsXUserTypes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPermissionsXUserTypes"));

                    b.Property<int>("FKIdPermission")
                        .HasColumnType("int");

                    b.Property<int>("FKIdUserType")
                        .HasColumnType("int");

                    b.HasKey("IdPermissionsXUserTypes");

                    b.HasIndex("FKIdPermission");

                    b.HasIndex("FKIdUserType");

                    b.ToTable("PermissionsXUserTypes");
                });

            modelBuilder.Entity("SabanaBus.Modelo.Route", b =>
                {
                    b.Property<int>("IdRoute")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRoute"));

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<TimeSpan>("EstimatedDuration")
                        .HasColumnType("time");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RouteName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdRoute");

                    b.ToTable("Route");
                });

            modelBuilder.Entity("SabanaBus.Modelo.Schedule", b =>
                {
                    b.Property<int>("IDSchedule")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDSchedule"));

                    b.Property<TimeSpan>("ArrivalTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("DepartureTime")
                        .HasColumnType("time");

                    b.Property<int>("FkIDRoute")
                        .HasColumnType("int");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("IDSchedule");

                    b.HasIndex("FkIDRoute");

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("SabanaBus.Modelo.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUser"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("FKIdUserType")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdUser");

                    b.HasIndex("FKIdUserType");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SabanaBus.Modelo.UserType", b =>
                {
                    b.Property<int>("IdUserType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUserType"));

                    b.Property<string>("UserTypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdUserType");

                    b.ToTable("UserType");
                });

            modelBuilder.Entity("SabanaBus.Modelo.Assignment", b =>
                {
                    b.HasOne("SabanaBus.Modelo.Bus", "Bus")
                        .WithMany("Assignment")
                        .HasForeignKey("FkBusID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SabanaBus.Modelo.Route", "Route")
                        .WithMany("Assignment")
                        .HasForeignKey("FkRouteID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Bus");

                    b.Navigation("Route");
                });

            modelBuilder.Entity("SabanaBus.Modelo.Notification", b =>
                {
                    b.HasOne("SabanaBus.Modelo.Schedule", "Schedule")
                        .WithMany("Notifications")
                        .HasForeignKey("FkIdSchedule")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("SabanaBus.Modelo.PermissionsXUserTypes", b =>
                {
                    b.HasOne("SabanaBus.Modelo.Permissions", "Permissions")
                        .WithMany("PermissionsXUserType")
                        .HasForeignKey("FKIdPermission")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SabanaBus.Modelo.UserType", "UserType")
                        .WithMany("PermissionsXUserType")
                        .HasForeignKey("FKIdUserType")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Permissions");

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("SabanaBus.Modelo.Schedule", b =>
                {
                    b.HasOne("SabanaBus.Modelo.Route", "Route")
                        .WithMany("Schedules")
                        .HasForeignKey("FkIDRoute")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");
                });

            modelBuilder.Entity("SabanaBus.Modelo.User", b =>
                {
                    b.HasOne("SabanaBus.Modelo.UserType", "UserType")
                        .WithMany("User")
                        .HasForeignKey("FKIdUserType")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("SabanaBus.Modelo.Bus", b =>
                {
                    b.Navigation("Assignment");
                });

            modelBuilder.Entity("SabanaBus.Modelo.Permissions", b =>
                {
                    b.Navigation("PermissionsXUserType");
                });

            modelBuilder.Entity("SabanaBus.Modelo.Route", b =>
                {
                    b.Navigation("Assignment");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("SabanaBus.Modelo.Schedule", b =>
                {
                    b.Navigation("Notifications");
                });

            modelBuilder.Entity("SabanaBus.Modelo.UserType", b =>
                {
                    b.Navigation("PermissionsXUserType");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
