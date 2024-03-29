using Microsoft.EntityFrameworkCore;
using pb_projekt.Models;

namespace pb_projekt.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
}