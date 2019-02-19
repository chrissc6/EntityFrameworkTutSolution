using Microsoft.EntityFrameworkCore; // add using statement
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkTut.Models
{
    public class AppDbContext : DbContext  //add pub, and inher dbc (context name like exceptions)
    {
        //one of these for every table you want to access
        public DbSet<Customer> Customers { get; set; } //needs to be public 
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) //needs to be protected
        {
                        //@ allows us to use backslash
            var connStr = @"server=localhost\sqlexpress;database=SalesDb2;Trusted_connection=true";
            builder.UseSqlServer(connStr);
        }
    }
}
