using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_Test.DB
{
    public class MyContext : DbContext
    {
        private string cs = "Server=localhost;database=TestWPF;Trusted_Connection=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(cs);
        }
        public DbSet<User> users { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<Storage> storage { get; set; }
    }
}
