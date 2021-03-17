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
            var databaseUrl = "postgres://nhbrhxkspooyqm:21770d47867aed6db21ce23746eb49b0c83a2af22a8bb9eb7dab417bd2d94215@ec2-34-252-251-16.eu-west-1.compute.amazonaws.com:5432/d717f5uoub6hft";
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/')
            };

            optionsBuilder.UseNpgsql(builder.ToString());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Anek> Aneks { get; set; }
    }
}
