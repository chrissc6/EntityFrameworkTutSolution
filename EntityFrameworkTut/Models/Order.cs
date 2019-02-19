using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityFrameworkTut.Models
{
    public class Order //add pub
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [StringLength(1000)]
        [Required]
        public string Description { get; set; }
        public double Total { get; set; } = 0; //real world this would be a dec
        public int CustomerId { get; set; } //ef will assume this is a fk to customer because of the name



        public Order() //add def ctor
        {

        }
    }
}
