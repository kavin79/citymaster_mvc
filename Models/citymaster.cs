using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace citymaster.Models
{
    public class citymaster
    {
        [Key]
        public int cityid { get; set; }

        public string cityname { get; set; }

        [ForeignKey("state")]
        public int intstateid { get; set; }

        public virtual statemaster state { get; set; }
    }
}