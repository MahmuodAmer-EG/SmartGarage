using Microsoft.AspNetCore.Identity;
using SmartGarage.Models;
using System.ComponentModel.DataAnnotations;

namespace SmartGarage.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public DateTimeOffset LastInvoiceDate { get; set; }

        public decimal Balance { get; set; }

        public CarState CarState { get; set; }

        public CarPlace? CarPlace{ get; set; }

        public int Units { get; set; }

        public int LastInvoiceId { get; set; }

        public List<Invoice>? Invoices { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}