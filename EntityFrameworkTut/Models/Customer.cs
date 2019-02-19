using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityFrameworkTut.Models
{
    public class Customer //added public
    {
        public int Id { get; set; }
        [StringLength(50)] //setting length, mods what is below "this is for name"
        [Required] //same as not null, "also for name"
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; } //nullable



        public Customer() //need to have public ctor that is the default
        {

        }
    }
}
