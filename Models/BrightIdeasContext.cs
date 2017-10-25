using Microsoft.EntityFrameworkCore;

namespace BrightIdeas.Models
{
    public class BrightIdeasContext: DbContext
    {
        public BrightIdeasContext(DbContextOptions<BrightIdeasContext> options):base(options){}
        public DbSet<User> users {get;set;}
        public DbSet<Idea> ideas {get;set;}
        public DbSet<Subscribtion> subscribtions {get;set;}
    }
}