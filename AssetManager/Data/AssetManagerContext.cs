using AssetManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace AssetManager.Data;

public class AssetManagerContext : DbContext
{
    public AssetManagerContext(DbContextOptions<AssetManagerContext> options) : base(options)
    {    
    }
    public DbSet<Person> People { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Order> Orders { get; set; }

    //protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    //{
    //    configurationBuilder.Properties<List<ProductOrder>>().HaveConversion<ProductOrderConverter>();

    //    base.ConfigureConventions(configurationBuilder);
    //}

    //private class ProductOrderConverter : ValueConverter<List<ProductOrder>, string>
    //{
    //    public ProductOrderConverter() : base(v => JsonConvert.SerializeObject(v),
    //                                          v => v != null ? (List<ProductOrder>)JsonConvert.DeserializeObject(v) : new List<ProductOrder>())
    //    {

    //    }
    //}
    //example:
    //protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    //{
    //    configurationBuilder.Properties<List<string>>().HaveConversion<StringListConverter>();

    //    base.ConfigureConventions(configurationBuilder);
    //}

    //private class StringListConverter : ValueConverter<List<string>, string>
    //{
    //    public StringListConverter() : base(v => string.Join(", ", v!),
    //                                        v => v.Split(',', StringSplitOptions.TrimEntries).ToList())
    //    {

    //    }
    //}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(p =>
        {
            p.ToTable("Person");
        }
        );

        modelBuilder.Entity<Asset>(a =>
        {
            a.ToTable("Asset");
        }
        );

        modelBuilder.Entity<Order>(o =>
        {
            o.ToTable("Order");
            o.HasOne(o => o.Purchaser).WithMany().HasForeignKey(o => o.PurchaserId).OnDelete(DeleteBehavior.NoAction);
            o.HasOne(o => o.Approver).WithMany().HasForeignKey(o => o.ApproverId).OnDelete(DeleteBehavior.NoAction);
            o.OwnsMany(o => o.Products);
        });

        base.OnModelCreating(modelBuilder);
    }
}
