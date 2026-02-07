using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArasvaAssignment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArasvaAssignment.Persistence.Contexts
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<BorrowTransactions> BorrowTransactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BookCopy> BookCopys { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PK for Setting
            modelBuilder.Entity<Setting>()
                .HasKey(s => s.KeyName);

            // RELATION → BorrowTransaction → Book
            modelBuilder.Entity<BorrowTransactions>()
                .HasOne(b => b.Book)
                .WithMany(t => t.BorrowTransactions)
                .HasForeignKey(b => b.BookId);

            // RELATION → BorrowTransaction → Member
            modelBuilder.Entity<BorrowTransactions>()
                .HasOne(m => m.Member)
                .WithMany(t => t.BorrowTransactions)
                .HasForeignKey(m => m.MemberId); 

            modelBuilder.Entity<Book>()
                    .HasIndex(b => b.ISBN)
                    .IsUnique();

            modelBuilder.Entity<Book>()
                .HasIndex(b => b.Title)
                .IsUnique();

            modelBuilder.Entity<Member>()
        .HasIndex(m => m.Email)
        .IsUnique();

            modelBuilder.Entity<Member>()
                .HasIndex(m => m.Mobile)
                .IsUnique();

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Reviews");

                entity.HasKey(r => r.ReviewId);

                entity.Property(r => r.ReviewId)
          .HasDefaultValueSql("NEWID()")
          .ValueGeneratedOnAdd();

                entity.Property(r => r.Rating).IsRequired();
                entity.Property(r => r.Comment).HasMaxLength(1000);

                entity.HasOne(r => r.Book)
                      .WithMany(b => b.Reviews)
                      .HasForeignKey(r => r.BookId);

                entity.HasOne(r => r.Member)
                      .WithMany(m => m.Reviews)
                      .HasForeignKey(r => r.MemberId);

                entity.HasIndex(r => r.BookId);
                entity.HasIndex(r => r.MemberId);
            });


            modelBuilder.Entity<BookCopy>()
                .HasIndex(m => m.Barcode)
                .IsUnique();

            modelBuilder.Entity<BookCopy>()
                .Property(bc => bc.Status)
                .HasConversion<string>();
        }
    }
}
