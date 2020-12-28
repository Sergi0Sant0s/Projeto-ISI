using DATA.Entities;
using DATA.Jwt;
using Microsoft.EntityFrameworkCore;

namespace DATA.Context
{

    public class MyContext : DbContext
    {

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<MapOfGame> MapOfGame { get; set; }
        public DbSet<StatPlayerOnMap> StatPlayerOnMap { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Game>()
            .HasOne(p => p.Event)
            .WithMany(b => b.Games)
            .HasForeignKey(p => p.EventId)
            .HasPrincipalKey(c => c.EventId);

            modelBuilder.Entity<Game>()
            .HasOne(p => p.TeamA)
            .WithMany(b => b.GamesTeamA)
            .HasForeignKey(p => p.TeamAId)
            .HasPrincipalKey(c => c.TeamId);

            modelBuilder.Entity<Game>()
            .HasOne(p => p.TeamB)
            .WithMany(b => b.GamesTeamB)
            .HasForeignKey(p => p.TeamBId)
            .HasPrincipalKey(c => c.TeamId);

            modelBuilder.Entity<Game>()
            .HasOne(p => p.TeamWinner)
            .WithMany(b => b.GamesTeamWinner)
            .HasForeignKey(p => p.TeamWinnerId)
            .HasPrincipalKey(c => c.TeamId);

            modelBuilder.Entity<User>().HasData(new User[] {
                new User() {UserId = 1, Username = "Admin", Password = "Admin",Email="qwerty@qwerty.qwerty", Role = UserRoles.Admin },
                new User() {UserId = 2, Username = "User", Password = "User",Email="qwerty@qwerty.qwerty", Role = UserRoles.User } 
            });
        }
    }
}
