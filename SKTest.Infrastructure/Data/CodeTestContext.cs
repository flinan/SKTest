using Microsoft.EntityFrameworkCore;
using SKTest.Db.Entities;

namespace SKTest.Infrastructure.Data
{
    public class CodeTestContext: DbContext
    {
        public CodeTestContext(DbContextOptions<CodeTestContext> options): base(options) { }

        public DbSet<User> Users => Set<User>();
    }
}
