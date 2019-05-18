using Microsoft.EntityFrameworkCore;
using PSK.Domain;

namespace PSK.Persistence
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> contextOptions) : base(contextOptions)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Trip> Trips { get; set; }

        public virtual DbSet<TripEmployee> TripEmployees { get; set; }

        public virtual DbSet<AccommodationReservation> AccommodationReservations { get; set; }

        public virtual DbSet<Accommodation> Accommodations { get; set; }

        public virtual DbSet<Office> Offices { get; set; }

        public virtual DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employees");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.HasIndex(x => x.Username).IsUnique();

                entity.Property(x => x.Version).IsRowVersion();
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.ToTable("Trips");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

                entity.Property(x => x.Version).IsRowVersion();

                entity.HasOne(x => x.StartLocation);
                entity.HasOne(x => x.EndLocation);
                entity.HasOne(x => x.Organizer).WithMany(x => x.OrganizedTrips);
            });

            modelBuilder.Entity<TripEmployee>(entity =>
            {
                entity.ToTable("TripEmployees");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.Property(x => x.CarReservationStatus).HasConversion<string>();
                entity.Property(x => x.PlaneTicketStatus).HasConversion<string>();
                entity.Property(x => x.CarReservationPrice).HasColumnType("decimal(18,2)");
                entity.Property(x => x.PlaneTicketPrice).HasColumnType("decimal(18,2)");

                entity.Property(x => x.Version).IsRowVersion();

                entity.HasOne(x => x.Employee).WithMany(x => x.Trips);
                entity.HasOne(x => x.Trip).WithMany(x => x.Employees);
                entity.HasOne(x => x.AccommodationReservation).WithOne(x => x.TripEmployee)
                    .HasForeignKey<AccommodationReservation>(x => x.TripEmployeeId);
            });

            modelBuilder.Entity<AccommodationReservation>(entity =>
            {
                entity.ToTable("AccommodationReservations");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.Property(x => x.Status).HasConversion<string>();
                entity.Property(x => x.Price).HasColumnType("decimal(18,2)");

                entity.Property(x => x.Version).IsRowVersion();

                entity.HasOne(x => x.Accommodation).WithMany(x => x.Reservations);
            });

            modelBuilder.Entity<Accommodation>(entity =>
            {
                entity.ToTable("Accommodations");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

                entity.Property(x => x.Version).IsRowVersion();

                entity.HasOne(x => x.Address);
                entity.HasOne(x => x.Office).WithMany(x => x.Accommodations);
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Addresses");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

                entity.Property(x => x.Version).IsRowVersion();
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.ToTable("Offices");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

                entity.Property(x => x.Version).IsRowVersion();

                entity.HasOne(x => x.Address);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}