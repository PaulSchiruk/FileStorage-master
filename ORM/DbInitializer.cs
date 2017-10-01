using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ORM
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            base.Seed(context);

            //context.Roles.Add(new Models.Role { Name = "user"});
            //context.Roles.Add(new Models.Role { Name = "admin" });
            //context.Roles.Add(new Models.Role { Name = "moderator" });
            //context.Roles.Add(new Models.Role { Name = "premium user" });

            context.SaveChanges();
        }
    }
}
