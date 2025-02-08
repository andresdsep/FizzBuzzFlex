using FizzBuzzFlex.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzFlex.EF.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<Game> Games => Set<Game>();

    public DbSet<Match> Matches => Set<Match>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<DivisorLabel>().Property(e => e.Id).ValueGeneratedNever();
        builder.Entity<Game>().Property(e => e.Id).ValueGeneratedNever();
        builder.Entity<Game>(entity =>
        {
            entity.HasMany(e => e.DivisorLabels).WithOne().HasForeignKey(e => e.GameId);
        });

        builder.Entity<Prompt>().Property(e => e.Id).ValueGeneratedNever();
        builder.Entity<Match>().Property(e => e.Id).ValueGeneratedNever();
        builder.Entity<Match>(entity =>
        {
            entity.HasMany(e => e.Prompts).WithOne().HasForeignKey(e => e.MatchId);
        });
    }
}