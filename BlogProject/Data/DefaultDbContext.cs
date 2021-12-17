using Microsoft.EntityFrameworkCore;
using BlogProject.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlogProject.Data
{
    public class DefaultDbContext : IdentityDbContext<ApplicationUser,IdentityRole,string>
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(new IdentityRole[]
            {
                new IdentityRole()
                {
                    
                    Name="AdminStrator",
                    NormalizedName="ADMINSTRATOR"
                },
                new IdentityRole()
                {
                    
                    Name="Normal",
                    NormalizedName="NORMAL"
                },
            });
        } 
        public DbSet<Blog> blogs { get; set; }
        public DbSet<BlogPictures> pictures { set; get; }
        public DbSet<Category> categories { set; get; }
        public DbSet<Comment> comments { set; get; }
        public DbSet<BlogCategory> blogCategories { set; get; }
    }
}
