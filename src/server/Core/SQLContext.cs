
using System;
using System.Security.Cryptography.X509Certificates;
using Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Server.Infrastructure;

namespace Server.Core
{
    public class SQLContext : DbContext
    {
        public SQLContext(DbContextOptions<SQLContext> options)
            : base(options)
        { }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(x =>
            {
                x.Property<int>("Id")
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                x.Property<string>("UserName")
                    .IsRequired()
                    .HasMaxLength(30);
            });
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<User>().HasMany(u => u.Cards);
            
            modelBuilder.Entity<Card>(x =>
            {
                x.Property<int>("Id")
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                x.Property<Currency>("Currency")
                    .IsRequired();
                
                x.Property<CardType>("CardType")
                    .IsRequired();
                
                x.Property<DateTime>("DTOpenCard")
                    .IsRequired();
                
                x.Property<int>("ValidityYear")
                    .IsRequired();
                
                x.HasOne(c => c.User).WithMany(u => u.Cards);
                x.HasMany(c => c.Transactions);
            });
            modelBuilder.Entity<Card>().HasKey(c => c.Id);
            modelBuilder.Entity<Card>().HasIndex(c => c.CardNumber).IsUnique();
            
            modelBuilder.Entity<Transaction>(x =>
            {
                x.Property<int>("Id")
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                x.Property<string>("CardFromNumber");

                x.Property<string>("CardToNumber")
                    .IsRequired();

                x.Property<DateTime>("DateTime")
                    .IsRequired();

                x.Property<decimal>("Sum")
                    .IsRequired();
            });
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
        }
    }
}