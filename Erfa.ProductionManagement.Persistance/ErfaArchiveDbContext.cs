using Microsoft.EntityFrameworkCore;

namespace Erfa.ProductionManagement.Persistance
{
    public class ErfaArchiveDbContext : ErfaDbContext
    {
        public ErfaArchiveDbContext(DbContextOptions<ErfaDbContext> options) : base(options)
        {

        }
    }
}
