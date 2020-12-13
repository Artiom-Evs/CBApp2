using System;
using System.Collections.Generic;
using System.Text;

using CBApp2.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CBApp2.Infrastructure.Services
{
    public class DataContext : DbContext
    {
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DataContext()
        {
            this.Database.EnsureCreated();
            //this.Database.Migrate();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string LocalFolderPath = System.IO.Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.Personal), "database.db");

        optionsBuilder.UseSqlite($"Filename={LocalFolderPath}");
        }
    }
}
