using AnekProvider.DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnekProvider.DataModels.Repositories
{
    public class AnekContext : DbContext
    {
        public AnekContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var databaseUrl =  "postgres://uolitwcljvprqd:07c92d8926b5133b4b064137421dc1a78ac9322373f97729c1049cb205fddf05@ec2-54-247-158-179.eu-west-1.compute.amazonaws.com:5432/d9m6sj9s0k74d3";
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };

            optionsBuilder.UseNpgsql(builder.ToString());
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CustomAnek>();
            builder.Entity<ParsableAnek>();

            builder
                .Entity<BaseAnek>()
                .HasOne<User>()
                .WithMany(user => user.Aneks)
                .HasForeignKey(thp => thp.UserID);

            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BaseAnek> Aneks { get; set; }
    }
}
