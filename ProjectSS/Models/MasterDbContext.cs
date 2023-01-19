using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjectSS.Models
{
    public class MasterDbContext:IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<ApplicationRole> AspNetRoles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        
        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
        {
            
        }
    }
}