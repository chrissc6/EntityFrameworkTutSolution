using EntityFrameworkTut.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkTut
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Customer> customers = null;

            using (var db = new AppDbContext())  //instance of the context, in the using statement no need for dispose
            {
                //to get all users
                customers = db.Customers  //context.prop.
                    .Where(c => c.Active == true)
                    .OrderBy(c => c.Name)
                    .ToList();

                foreach (var i in customers)
                {
                    Console.WriteLine(i.Name);
                }

                //to get by primary key
                var id = 3;
                var co = db.Customers
                    .Find(id); //find is a method you can use to get by pk
                Console.WriteLine($"GetByPK = {co.Name}");


                //insert, add data
                var insertCu = new Customer()
                {
                    Id = 0, Name = "NewName",
                    Active = true, DateCreated = DateTime.Now
                };
                var hasName = db.Customers.Any(c => c.Name == "NewName");
                if(!hasName) //check to make sure name doesnt exist
                {
                    db.Customers.Add(insertCu); //like staging changes, must be done for every row
                }
                


                //update
                var ChangeId = db.Customers.Find(2);
                ChangeId.Name = "P&G"; //no add needed


                //delete
                var firstFoundName = db.Customers
                    .FirstOrDefault(C => C.Name == "NewName");
                if(firstFoundName != null)
                {
                    db.Customers.Remove(firstFoundName);
                }



                db.SaveChanges(); // actually saves the into the database, can be done for many rows
                //insertCu.Name = "Name"
            }

            
        }
    }
}
