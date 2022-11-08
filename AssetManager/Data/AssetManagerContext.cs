using Microsoft.EntityFrameworkCore;
using AssetManager.Models;

namespace AssetManager.Data;

public class AssetManagerContext : DbContext
{
    public DbSet<Person> People { get; set; } = null!;

    
}
