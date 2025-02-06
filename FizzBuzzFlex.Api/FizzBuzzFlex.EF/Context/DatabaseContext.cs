using FizzBuzzFlex.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzFlex.EF.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public required DbSet<Game> Games { get; set; }

    public required DbSet<Match> Matches { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Game>(entity =>
        {
            entity.HasMany(e => e.DivisorLabels).WithOne().HasForeignKey(e => e.GameId);
        });

        builder.Entity<Match>(entity =>
        {
            entity.HasMany(e => e.Prompts).WithOne().HasForeignKey(e => e.MatchId);
        });
    }
}