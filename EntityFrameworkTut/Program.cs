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
            List<Order> orders = null;

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



                //db.SaveChanges(); added new one to end 
                // actually saves the into the database, can be done for many rows
                //insertCu.Name = "Name"

                

                //orders add
                var insertOr = new Order()
                {
                    Id = 0,
                    CustomerId = 4,
                    Description = "Order no1",
                    Total = 1500
                };
                db.Orders.Add(insertOr);
                var insertOr2 = new Order()
                {
                    Id = 0,
                    CustomerId = 4,
                    Description = "Order no2",
                    Total = 2500
                };
                db.Orders.Add(insertOr2);
                var insertOr3 = new Order()
                {
                    Id = 0,
                    CustomerId = 7,
                    Description = "Order no1",
                    Total = 3500
                };
                //db.Orders.Add(insertOr3);
                var insertOr4 = new Order()
                {
                    Id = 0,
                    CustomerId = 7,
                    Description = "Order no2",
                    Total = 4500
                };
                //db.Orders.Add(insertOr4);
                var insertOr5 = new Order()
                {
                    Id = 0,
                    //CustomerId = 2,
                    CustomerId = db.Customers.SingleOrDefault(c => c.Name == "Amazon").Id,
                    Description = "Order no1",
                    Total = 5500
                };
                //db.Orders.Add(insertOr5);
                db.Orders.AddRange(new[] { insertOr3, insertOr4, insertOr5 }); ;
                
                
                orders = db.Orders
                    .ToList();
                //orders.ForEach(i => Console.WriteLine(i));
                foreach (var i in orders)
                {
                    Console.WriteLine(i.ToString());
                }

                var idx = 2;
                var od = db.Orders
                    .Find(idx); 
                Console.WriteLine($"GetByPK = {od}");

                db.SaveChanges();

                //get all orders < 5000 sort by total desc
                var orders2 = db.Orders
                    .Where(t => t.Total < 5000)
                    .OrderByDescending(t => t.Total)
                    .ToList();
                foreach (var i in orders2)
                {
                    Console.WriteLine(i.ToString());
                }


                //sum all orders from any 1 customer and display cus name and order total
                var cus = "Kroger";
                var orders3 = db.Orders
                    .Where(o => o.Customer.Name == cus)
                    .Sum(i => i.Total);
                    
                Console.WriteLine($"{cus} Total = {orders3:C}");
            }


        }
    }
}
