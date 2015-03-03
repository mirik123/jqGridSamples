namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication2.DevEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplication2.DevEntities Context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            Context.ClientStatus.AddRange(new ClientStatus[] {
                new ClientStatus() { Id = 0, color = "transparent", descr = "started" },
                new ClientStatus() { Id = 1, color = "red", descr = "failed" },
                new ClientStatus() { Id = 2, color = "yellow", descr = "finished" }
            });

            Context.ClientType.AddRange(new ClientType[] {
                new ClientType() { Id = 0, descr = "seller" },
                new ClientType() { Id = 1, descr = "buyer" },
                new ClientType() { Id = 2, descr = "manager" },
                new ClientType() { Id = 3, descr = "worker" },
                new ClientType() { Id = 4, descr = "inspector" }
            });

            Context.Client.AddRange(new Client[] {
                new Client() { Id = 0, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "232323", addr_city = "", addr_home = "", addr_street = "", cltype = 0, status = 0 },
                new Client() { Id = 1, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "4132546", addr_city = "", addr_home = "", addr_street = "", cltype = 1, status = 0 },
                new Client() { Id = 2, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "46363", addr_city = "", addr_home = "", addr_street = "", cltype = 2, status = 0 },
                new Client() { Id = 3, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "43636", addr_city = "", addr_home = "", addr_street = "", cltype = 3, status = 1 },
                new Client() { Id = 4, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "346346", addr_city = "", addr_home = "", addr_street = "", cltype = 4, status = 1 },
                new Client() { Id = 5, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "611", addr_city = "", addr_home = "", addr_street = "", cltype = 0, status = 1 },
                new Client() { Id = 6, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "143641", addr_city = "", addr_home = "", addr_street = "", cltype = 1, status = 1 },
                new Client() { Id = 7, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "16463", addr_city = "", addr_home = "", addr_street = "", cltype = 2, status = 1 },
                new Client() { Id = 8, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "146466", addr_city = "", addr_home = "", addr_street = "", cltype = 3, status = 1 },
                new Client() { Id = 9, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "16461", addr_city = "", addr_home = "", addr_street = "", cltype = 4, status = 1 },
                new Client() { Id = 10, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "1646146", addr_city = "", addr_home = "", addr_street = "", cltype = 0, status = 2 },
                new Client() { Id = 11, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "79949", addr_city = "", addr_home = "", addr_street = "", cltype = 1, status = 2 },
                new Client() { Id = 12, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "49779", addr_city = "", addr_home = "", addr_street = "", cltype = 1, status = 0 },
                new Client() { Id = 13, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "49", addr_city = "", addr_home = "", addr_street = "", cltype = 1, status = 0 },
                new Client() { Id = 14, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "497494", addr_city = "", addr_home = "", addr_street = "", cltype = 2, status = 0 },
                new Client() { Id = 15, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "35146", addr_city = "", addr_home = "", addr_street = "", cltype = 2, status = 2 },
                new Client() { Id = 16, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "46327", addr_city = "", addr_home = "", addr_street = "", cltype = 2, status = 0 },
                new Client() { Id = 17, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "157547", addr_city = "", addr_home = "", addr_street = "", cltype = 3, status = 0 },
                new Client() { Id = 18, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "25757", addr_city = "", addr_home = "", addr_street = "", cltype = 3, status = 0 },
                new Client() { Id = 19, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "27275257", addr_city = "", addr_home = "", addr_street = "", cltype = 4, status = 2 },
                new Client() { Id = 20, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "27254", addr_city = "", addr_home = "", addr_street = "", cltype = 0, status = 0 },
                new Client() { Id = 21, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "25727", addr_city = "", addr_home = "", addr_street = "", cltype = 0, status = 0 },
                new Client() { Id = 22, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "25772", addr_city = "", addr_home = "", addr_street = "", cltype = 0, status = 2 },
                new Client() { Id = 23, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "27527", addr_city = "", addr_home = "", addr_street = "", cltype = 0, status = 2 },
                new Client() { Id = 24, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "798070", addr_city = "", addr_home = "", addr_street = "", cltype = 0, status = 0 },
                new Client() { Id = 25, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "15415", addr_city = "", addr_home = "", addr_street = "", cltype = 1, status = 0 },
                new Client() { Id = 26, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "6876809", addr_city = "", addr_home = "", addr_street = "", cltype = 0, status = 1 },
                new Client() { Id = 27, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "878456", addr_city = "", addr_home = "", addr_street = "", cltype = 0, status = 1 },
                new Client() { Id = 28, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "254568", addr_city = "", addr_home = "", addr_street = "", cltype = 0, status = 0 },
                new Client() { Id = 29, firstname = "firstname", lastname = "lastname", phone_cell = "", ssn = "15568", addr_city = "", addr_home = "", addr_street = "", cltype = 0, status = 0 }
            });

            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb.ToString(), ex); 
            }
        }
    }
}
