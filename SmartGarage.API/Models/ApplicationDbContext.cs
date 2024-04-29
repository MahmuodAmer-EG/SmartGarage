using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartGarage.Models;

namespace SmartGarage.API.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User_QrCode> User_QrCode { get; set; }

        public DbSet<CarPlace> CarPlaces { get; set; }

        public DbSet<UnitTransaction> UnitTransactions { get; set; }
    }

    public class CarPlace
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int Floor { get; set; }
        public bool Available { get; set; }=false;
    }
    public class UnitTransaction
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
    }

}