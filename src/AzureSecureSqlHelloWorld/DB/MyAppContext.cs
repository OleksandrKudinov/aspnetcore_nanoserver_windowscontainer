using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AzureSecureSqlHelloWorld.DB
{
    public class MyAppContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public MyAppContext(DbContextOptions options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("db_students");
            base.OnModelCreating(modelBuilder);
        }
    }
}
