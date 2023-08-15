using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Models.Models;
using System.Reflection.Emit;


namespace SimpleBlog.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlogPost>()
            .HasOne(bp => bp.Author)
            .WithMany(au => au.BlogPosts)
            .HasForeignKey(bp => bp.AuthorId)
            .IsRequired();

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.BlogPost)
                .WithMany(bp => bp.Comments)
                .HasForeignKey(c => c.BlogPostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subscriber>()
                .Property(s => s.DateSubscribed)
                .IsRequired();

            modelBuilder.Entity<Subscriber>()
            .Property(s => s.DateUnSubscribed)
            .IsRequired(false);



        }
    }
}

