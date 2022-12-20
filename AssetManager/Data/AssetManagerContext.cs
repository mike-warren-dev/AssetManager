using AssetManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace AssetManager.Data;

public class AssetManagerContext : IdentityDbContext<ApplicationUser>
{
    public AssetManagerContext(DbContextOptions<AssetManagerContext> options) : base(options)
    {    
    }
    public DbSet<Person> People { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DbSet<Dict> Dicts { get; set; }

    public DbSet<DictOption> DictOptions { get; set; }  


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        modelBuilder.Entity<Person>(p =>
        {
            p.ToTable("Person");
        });

        modelBuilder.Entity<Asset>(a =>
        {
            a.ToTable("Asset");
            a.HasOne(a => a.AssetType).WithMany().HasForeignKey(o => o.AssetTypeId).OnDelete(DeleteBehavior.NoAction);
            a.HasOne(a => a.Model).WithMany().HasForeignKey(o => o.ModelId).OnDelete(DeleteBehavior.NoAction);
            a.HasOne(a => a.Site).WithMany().HasForeignKey(o => o.SiteId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Order>(o =>
        {
            o.ToTable("Order");
            o.HasOne(o => o.Purchaser).WithMany().HasForeignKey(o => o.PurchaserId).OnDelete(DeleteBehavior.NoAction);
            o.HasOne(o => o.Approver).WithMany().HasForeignKey(o => o.ApproverId).OnDelete(DeleteBehavior.NoAction);
            o.HasOne(o => o.Vendor).WithMany().HasForeignKey(o => o.VendorId).OnDelete(DeleteBehavior.NoAction);
            o.OwnsMany(o => o.Products);
        });

        modelBuilder.Entity<Dict>(d =>
        {
            d.ToTable("Dict");
            //d.HasMany(d => d.DictOptions).WithOne().HasForeignKey(d => d.DictOptionId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<DictOption>(d =>
        {
            d.ToTable("DictOption");
            //d.HasOne(d => d.Dict).WithMany().HasForeignKey(d => d.DictId).OnDelete(DeleteBehavior.NoAction);
        });

        base.OnModelCreating(modelBuilder);
    }
}
