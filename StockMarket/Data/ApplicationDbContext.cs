using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockMarket.Models;

namespace StockMarket.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options )
            : base(options)
        {
        }
        public DbSet<StockDisplay> stocks { get; set; }
    }
}