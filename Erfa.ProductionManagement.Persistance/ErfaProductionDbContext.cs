using Microsoft.EntityFrameworkCore;

namespace Erfa.ProductionManagement.Persistance
{
    public class ErfaProductionDbContext : ErfaDbContext
    {
        public ErfaProductionDbContext(DbContextOptions<ErfaDbContext> options) : base(options)
        {
        }
    }
}
