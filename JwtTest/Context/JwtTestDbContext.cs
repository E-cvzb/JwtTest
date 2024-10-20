using JwtTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtTest.Context
{
    public class JwtTestDbContext:DbContext
    {
        public JwtTestDbContext(DbContextOptions<JwtTestDbContext> options):base(options) 
        {


            
        }

        public DbSet<UserEntitiy> User => Set<UserEntitiy>();
    }
}
