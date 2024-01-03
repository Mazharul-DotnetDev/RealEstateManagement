using Microsoft.EntityFrameworkCore;

namespace RealEstateManagement.Models
{
    public class EstateContext : DbContext
    {
        public DbSet<Tenant> TenantTble { get; set; }
        public DbSet<Asset> AssetTble { get; set; }
        public DbSet<Owner> OwnerTble { get; set; }

        public EstateContext(DbContextOptions opt) : base(opt)
        {

        }
    }
}
