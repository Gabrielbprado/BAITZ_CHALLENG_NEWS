using AMS_News.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AMS_News.Infraestructure.Data;

public class AmsNewsContext(DbContextOptions<AmsNewsContext> opts) : DbContext(opts)
{
    public DbSet<Customers> Customers { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<Likes> Likes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AmsNewsContext).Assembly);
    }
}