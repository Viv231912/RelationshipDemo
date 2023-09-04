using Microsoft.EntityFrameworkCore;
using RelationshipDemo.Data.Models;

namespace RelationshipDemo.Data
{
    public class DataContext : DbContext
    {
        //Create Contructor DataContext, gets argument DbContextOptions with datacontext type 
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Backpack> Backpacks { get; set; }
        public DbSet<Weapon> Weapons { get; set; }  
        public DbSet<Faction> Factions { get; set; }    

    }
}
