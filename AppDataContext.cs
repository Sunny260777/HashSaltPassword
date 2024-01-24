using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashSaltPassword
{
    public class AppDataContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-87PCEUU\SQLEXPRESS;Initial Catalog=sunny;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");

        }

        public DbSet<User> Users { get; set; }
    }
}
