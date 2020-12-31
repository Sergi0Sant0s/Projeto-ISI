using DATA.Entities;
using DATA.Jwt;
using Microsoft.EntityFrameworkCore;
using System;

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
            .IsRequired(false)
            .HasForeignKey(p => p.TeamWinnerId)
            .HasPrincipalKey(c => c.TeamId);

            modelBuilder.Entity<Player>()
            .HasOne(p => p.Team)
            .WithMany(b => b.Players)
            .HasForeignKey(p => p.TeamId)
            .HasPrincipalKey(c => c.TeamId);

            modelBuilder.Entity<User>().HasData(new User[] {
                new User() {UserId = 1, Username = "Admin", Password = "Admin",Email="qwerty@qwerty.qwerty", Role = UserRoles.Admin },
                new User() {UserId = 2, Username = "User", Password = "User",Email="qwerty@qwerty.qwerty", Role = UserRoles.User } 
            });


            modelBuilder.Entity<Map>().HasData(new Map[]{
                new Map(){ MapId=1, Description = "Inferno"},
                new Map(){ MapId=2, Description = "Vertigo"},
                new Map(){ MapId=3, Description = "Dust2"},
                new Map(){ MapId=4, Description = "Overpass"},
                new Map(){ MapId=5, Description = "Train"}
                });

            modelBuilder.Entity<Player>().HasData(new Player[]{
                new Player(){PlayerId=1,Name = "Gabriel Toledo", Nationality = "Brazil", Age = 29, Nickname = "FalleN"},
                new Player(){PlayerId=2,Name = "Vito Giuseppe", Nationality = "Brazil", Age = 28, Nickname = "kNgV-"},
                new Player(){PlayerId=3,Name = "Nicolai Reedtz", Nationality = "Dinamarca", Age = 25, Nickname = "device"},
                new Player(){PlayerId=4,Name = "Peter Rasmussen", Nationality = "Dinamarca", Age = 27, Nickname = "dupreeh"},
                new Player(){PlayerId=5, Name = "Lukas Rossander", Nationality = "Dinamarca", Age = 25, Nickname = "gla1ve"}
                });

            modelBuilder.Entity<Team>().HasData(new Team[]{
                new Team(){TeamId=1, TeamName = "Mibr", TeamNationality = "Brazil", TeamRanking = 21},
                new Team(){TeamId=2, TeamName = "Astralis", TeamNationality = "Dinamarca", TeamRanking = 1},
                new Team(){TeamId=3, TeamName = "Natus Vincere", TeamNationality = "Russia", TeamRanking = 3},
                new Team(){TeamId=4, TeamName = "OG", TeamNationality = "Europa", TeamRanking = 7},
                new Team(){TeamId=5, TeamName = "Mousesports", TeamNationality = "Europa", TeamRanking = 9}
                });

            modelBuilder.Entity<Event>().HasData(new Event[]{
                new Event() { EventId = 1, EventName = "Blast New York", DateOfStart = DateTime.Now, DateOfEnd = DateTime.Now.AddDays(5) }
            });
        }
    }
}
