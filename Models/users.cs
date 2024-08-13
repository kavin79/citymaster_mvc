using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace citymaster.Models
{
    public class users
    {
        [Key]
        public int user_id { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Username required")]
        public string username { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password Required")]
        
        [DataType(DataType.Password)]
        public string password { get; set; }

        //[DisplayName("Remember Me")]
        public bool remember_me { get; set; }
    }
}