using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PSK.Domain;

namespace PSK.Persistence
{
    public interface IDataContext
    {
        DbSet<Trip> Trips { get; set; }

        DbSet<TripEmployee> TripEmployees { get; set; }

        DbSet<File> Files { get; set; }

        DbSet<AccommodationReservation> AccommodationReservations { get; set; }

        DbSet<Accommodation> Accommodations { get; set; }

        DbSet<Office> Offices { get; set; }

        DbSet<Address> Addresses { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}