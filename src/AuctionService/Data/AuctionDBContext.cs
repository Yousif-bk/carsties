using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public class AuctionDBContext : DbContext
{
    public AuctionDBContext(DbContextOptions<AuctionDBContext> options) : base(options)
    {

    }

    public DbSet<Auction> Auctions { get; set; }
    // public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auction>()
       .Property(a => a.CreatedAt)
       .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<Auction>()
            .Property(a => a.UpdatedAt)
            .HasDefaultValueSql("NOW()");
        modelBuilder.Entity<Auction>()
       .HasOne(a => a.Item)
       .WithOne(i => i.Auction)
       .HasForeignKey<Item>(i => i.AuctionId)
       .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Item>()
            .Property(i => i.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Auction>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Item>()
.Property(i => i.Id)
.HasDefaultValueSql("gen_random_uuid()");

    }
}