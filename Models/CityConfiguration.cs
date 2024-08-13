using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace citymaster.Models
{
    public class CityConfiguration : EntityTypeConfiguration<citymaster>
    {
        public CityConfiguration()
        {
            HasRequired(c => c.state).WithMany(s => s.cities).HasForeignKey(c => c.intstateid);
        }
    }
}