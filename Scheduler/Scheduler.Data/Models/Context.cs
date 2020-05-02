using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Scheduler.Data.Models
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Reminder> Reminder { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=52.206.157.109;Database=U051jM;Uid=U051jM;Pwd=53688407906;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");

                entity.HasIndex(e => e.CityId)
                    .HasName("cityId");

                entity.Property(e => e.AddressId)
                    .HasColumnName("addressId")
                    .HasColumnType("int(10)");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Address2)
                    .IsRequired()
                    .HasColumnName("address2")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CityId)
                    .HasColumnName("cityId")
                    .HasColumnType("int(10)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("createdBy")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("lastUpdate")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.LastUpdateBy)
                    .IsRequired()
                    .HasColumnName("lastUpdateBy")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("postalCode")
                    .HasColumnType("varchar(10)");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("address_ibfk_1");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("appointment");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("customerId");

                entity.HasIndex(e => e.UserId)
                    .HasName("userId");

                entity.Property(e => e.AppointmentId)
                    .HasColumnName("appointmentId")
                    .HasColumnType("int(10)");

                entity.Property(e => e.Contact)
                    .IsRequired()
                    .HasColumnName("contact")
                    .HasColumnType("text");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("createdBy")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customerId")
                    .HasColumnType("int(10)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.End)
                    .HasColumnName("end")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("lastUpdate")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.LastUpdateBy)
                    .IsRequired()
                    .HasColumnName("lastUpdateBy")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location")
                    .HasColumnType("text");

                entity.Property(e => e.Start)
                    .HasColumnName("start")
                    .HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("text");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("appointment_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("appointment_ibfk_2");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city");

                entity.HasIndex(e => e.CountryId)
                    .HasName("countryId");

                entity.Property(e => e.CityId)
                    .HasColumnName("cityId")
                    .HasColumnType("int(10)");

                entity.Property(e => e.City1)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CountryId)
                    .HasColumnName("countryId")
                    .HasColumnType("int(10)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("createdBy")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("lastUpdate")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.LastUpdateBy)
                    .IsRequired()
                    .HasColumnName("lastUpdateBy")
                    .HasColumnType("varchar(40)");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("city_ibfk_1");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.Property(e => e.CountryId)
                    .HasColumnName("countryId")
                    .HasColumnType("int(10)");

                entity.Property(e => e.Country1)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("createdBy")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("lastUpdate")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.LastUpdateBy)
                    .IsRequired()
                    .HasColumnName("lastUpdateBy")
                    .HasColumnType("varchar(40)");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.AddressId)
                    .HasName("addressId");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customerId")
                    .HasColumnType("int(10)");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.AddressId)
                    .HasColumnName("addressId")
                    .HasColumnType("int(10)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("createdBy")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasColumnName("customerName")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("lastUpdate")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.LastUpdateBy)
                    .IsRequired()
                    .HasColumnName("lastUpdateBy")
                    .HasColumnType("varchar(40)");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customer_ibfk_1");
            });

            modelBuilder.Entity<Reminder>(entity =>
            {
                entity.ToTable("reminder");

                entity.Property(e => e.ReminderId)
                    .HasColumnName("reminderId")
                    .HasColumnType("int(10)");

                entity.Property(e => e.AppointmentId)
                    .HasColumnName("appointmentId")
                    .HasColumnType("int(10)");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("createdBy")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReminderDate)
                    .HasColumnName("reminderDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Remindercol)
                    .IsRequired()
                    .HasColumnName("remindercol")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.SnoozeIncrement)
                    .HasColumnName("snoozeIncrement")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SnoozeIncrementTypeId)
                    .HasColumnName("snoozeIncrementTypeId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("createdBy")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("lastUpdate")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.LastUpdateBy)
                    .IsRequired()
                    .HasColumnName("lastUpdateBy")
                    .HasColumnType("varchar(40)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("userName")
                    .HasColumnType("varchar(50)");
            });
        }
    }
}
