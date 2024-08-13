using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace citymaster.Models
{
    public class LoginContext : DbContext
    {
        public LoginContext() : base("name=DefaultConnection")
        {
            
        }

        public DbSet<users> user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<users>().ToTable("users", "public");
            modelBuilder.Entity<users>().HasKey(model => model.user_id);
        }
    }
}