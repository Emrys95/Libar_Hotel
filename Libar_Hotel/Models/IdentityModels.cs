using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Libar_Hotel.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //dodato
        public DbSet<User> UserInfos { get; set; }
        
        //dodato
        public DbSet<RoomType> RoomTypes { get; set; }

        //dodato
        public DbSet<RoomStatus> RoomStatuses { get; set; }

        //dodato
        public DbSet<Room> Rooms { get; set; }

        //dodato
        public DbSet<ServiceType> ServiceTypes { get; set; }

        //dodato
        public DbSet<Reservation> Reservations { get; set; }

        //dodato
        public DbSet<RoomView> RoomViews { get; set; }

        //dodato
        public DbSet<ReservationRequest> ReservationRequests { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}