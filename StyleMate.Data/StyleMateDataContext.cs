using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using StyleMate.Data.EntityModels;
using System;

namespace StyleMate.Data
{
    public class StyleMateDataContext : DbContext
    {
        public StyleMateDataContext()
        {
        }

        public StyleMateDataContext(DbContextOptions<StyleMateDataContext> options) : base(options)
        {
        }

        public virtual DbSet<StyleMateGarment> StyleMateGarments { get; set; }
        public virtual DbSet<ImageUrl> ImageUrls { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //dataseed
            modelBuilder.Entity<StyleMateGarment>().HasData(new StyleMateGarment() { Id = 1, Name = "Werkt gw man" });
        }
    }
}