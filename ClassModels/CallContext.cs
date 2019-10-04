using Microsoft.EntityFrameworkCore;

namespace CallLog
{
    public class CallContext : DbContext
    {
        public DbSet<Call> Calls { get; set; }

        public CallContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=CallLog;Trusted_Connection=True;");
        }

    }
}
