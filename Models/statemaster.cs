using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace citymaster.Models
{
    public class statemaster
    {
        public int intstateid { get; set; }

        public string strstatename { get; set; }

        public virtual ICollection<citymaster> cities { get; set; }
    }
}