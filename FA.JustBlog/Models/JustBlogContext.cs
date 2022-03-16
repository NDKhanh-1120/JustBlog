using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FA.JustBlog.Models
{
    public partial class JustBlogContext : DbContext
    {
        public JustBlogContext()
            : base("name=JustBlogDb")
        {

        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);
        }
    }
}
