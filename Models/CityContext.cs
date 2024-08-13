using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace citymaster.Models
{
    public class CityContext : DbContext
    {

        public DbSet<citymaster> cities { get; set; }
        public DbSet<statemaster> states { get; set; }
        public CityContext() : base("name=DefaultConnection")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CityConfiguration());
            modelBuilder.Entity<citymaster>().ToTable("citymaster", "public");
            modelBuilder.Entity<citymaster>().HasKey(e => e.cityid);
            modelBuilder.Entity<statemaster>().ToTable("statemaster", "public");
            modelBuilder.Entity<statemaster>().HasKey(e => e.intstateid);
        }
    }
}