using DomainLayer.Models.AuthorModule;
using DomainLayer.Models.BookModule;
using DomainLayer.Models.CategoryModule;
using DomainLayer.Models.EmployeeModule;
using DomainLayer.Models.PaymentModule;
using DomainLayer.Models.PublisherModule;
using DomainLayer.Models.ShelfModule;
using DomainLayer.Models.UserModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
    {
        // DbSets
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<UserBorrow> UserBorrows { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Shelf> Shelves { get; set; }

        public DbSet<Payment> payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmpId); // Employee Primary Key
            modelBuilder.Entity<User>()
                .HasKey(u => u.SSN); // User Primary Key
            modelBuilder.Entity<Book>()
                .HasKey(b => b.BookId); // Book Primary Key
            modelBuilder.Entity<Author>()
                .HasKey(a => a.AuthorId); // Author Primary Key
            modelBuilder.Entity<Publisher>()
                .HasKey(p => p.Id); // Publisher Primary Key
            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id); // Category Primary Key
            modelBuilder.Entity<Shelf>()
                .HasKey(s => s.Code); // Shelf Primary Key

            // Decimal Precision Configuration

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Bonus)
                .HasPrecision(18, 2);
            modelBuilder.Entity<UserBorrow>()
                .Property(ub => ub.AmountOfMoney)
                .HasPrecision(18, 2);

            //Employee ↔ Employee (Self-referencing for Supervisor)
            modelBuilder.Entity<Employee>()
              .HasOne(e => e.Supervisor)
              .WithMany(e => e.Subordinates)
              .HasForeignKey(e => e.SupervisorId)
              .OnDelete(DeleteBehavior.Restrict);

            // Employee ↔ User
            modelBuilder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithMany(e => e.Users)
                .HasForeignKey(u => u.EmployeeId);


            // Book ↔ Author (Many-to-Many via BookAuthor)
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId }); // Composite Key
            modelBuilder.Entity<BookAuthor>()
             .HasOne(ba => ba.Book)
             .WithMany(b => b.BookAuthors)
             .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);


            // User ↔ Book (Many-to-Many via UserBorrow)
            modelBuilder.Entity<UserBorrow>()
                .HasKey(ub => ub.UserBorrowId); // Primary Key

            modelBuilder.Entity<UserBorrow>()
                .HasOne(ub => ub.User)
                .WithMany()
                .HasForeignKey(ub => ub.UserSSN);

            modelBuilder.Entity<UserBorrow>()
            .HasOne(ub => ub.Book)
            .WithMany()
            .HasForeignKey(ub => ub.BookId);

            modelBuilder.Entity<UserBorrow>()
                .HasOne(ub=>ub.Employee)
                .WithMany(e=>e.UserBorrows)
                .HasForeignKey(ub=>ub.EmployeeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Publisher-Book (One-to-Many):
            modelBuilder.Entity<Publisher>()
                .HasMany(p => p.Books)
                .WithOne(b => b.Publisher)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Category-Book (One-to-Many):
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Books)
                .WithOne(b => b.Category)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Shelf-Book (One-to-Many):
            modelBuilder.Entity<Shelf>()
                .HasMany(sh => sh.Books)
                .WithOne(b => b.Shelf)
                .HasForeignKey(b => b.ShelfCode)
                .OnDelete(DeleteBehavior.Restrict);

            #region Configure string max lengths
            modelBuilder.Entity<Publisher>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Category>()
                .Property(c => c.Cat_Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Shelf>()
                .Property(s => s.Code)
                .IsRequired()
                .HasMaxLength(50);
            #endregion
        }

    }
}
