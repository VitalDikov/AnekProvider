using AnekProvider.DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnekProvider.DataModels.Repositories
{
    public class AnekContext : DbContext
    {
        public AnekContext(DbContextOptions<AnekContext> options)
        : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Anek> Aneks { get; set; }
    }
}
