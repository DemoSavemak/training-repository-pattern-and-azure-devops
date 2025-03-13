using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using WebAPI.Model;


namespace WebAPI.Repository
{
    public partial class FusionDBContext : DbContext
    {
        public FusionDBContext(DbContextOptions options)
            : base(options)
        {
        }
        public virtual DbSet<Store> Store { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store", "dbo");
            });
        }
    }
    
}
