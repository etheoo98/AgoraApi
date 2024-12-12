using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Thread = Domain.Entities.Thread;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Forum> Forums { get; set; }
    public DbSet<Thread> Threads { get; set; }
    public DbSet<Comment> Comments { get; set; }
}