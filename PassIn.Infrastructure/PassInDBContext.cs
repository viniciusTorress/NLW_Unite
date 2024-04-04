﻿using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure.Entities;


namespace PassIn.Infrastructure
{
    public class PassInDBContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\vinia\\OneDrive\\Documentos\\PassInDb.db");
        }
    }
}
