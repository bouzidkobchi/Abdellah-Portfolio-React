﻿using Microsoft.EntityFrameworkCore;

namespace Abdellah_Portfolio_React.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .AddArticleEntity()
                .AddUserEntity()
                .AddProjectEntity();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config.GetSection("ConnectionStrings").GetSection("sqlite").Value;

            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
