using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Client.Models
{
    public partial class GamesContext : DbContext
    {
        public GamesContext()
        {
        }

        public GamesContext(DbContextOptions<GamesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Map> Maps { get; set; }
        public virtual DbSet<MapOfGame> MapOfGames { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<StatPlayerOnMap> StatPlayerOnMaps { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Games;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasIndex(e => e.EventId, "IX_Games_EventId");

                entity.HasIndex(e => e.TeamAid, "IX_Games_TeamAId");

                entity.HasIndex(e => e.TeamBid, "IX_Games_TeamBId");

                entity.HasIndex(e => e.TeamWinnerId, "IX_Games_TeamWinnerId");

                entity.Property(e => e.TeamAid).HasColumnName("TeamAId");

                entity.Property(e => e.TeamBid).HasColumnName("TeamBId");

                
            });

            modelBuilder.Entity<Map>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(20);
            });

            modelBuilder.Entity<MapOfGame>(entity =>
            {
                entity.ToTable("MapOfGame");

                entity.HasIndex(e => e.GameId, "IX_MapOfGame_GameId");

                entity.HasIndex(e => e.MapaId, "IX_MapOfGame_MapaId");

                entity.Property(e => e.TeamAresult).HasColumnName("TeamAResult");

                entity.Property(e => e.TeamBresult).HasColumnName("TeamBResult");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.MapOfGames)
                    .HasForeignKey(d => d.GameId);

                entity.HasOne(d => d.Mapa)
                    .WithMany(p => p.MapOfGames)
                    .HasForeignKey(d => d.MapaId);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.Facebook).HasMaxLength(100);

                entity.Property(e => e.Instagram).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.Nationality).HasMaxLength(30);

                entity.Property(e => e.Nickname).HasMaxLength(30);

                entity.Property(e => e.Twitter).HasMaxLength(100);
            });

            modelBuilder.Entity<StatPlayerOnMap>(entity =>
            {
                entity.ToTable("StatPlayerOnMap");

                entity.HasIndex(e => e.MapOfGameId, "IX_StatPlayerOnMap_MapOfGameId");

                entity.HasIndex(e => e.PlayerId, "IX_StatPlayerOnMap_PlayerId");

                entity.HasIndex(e => e.TeamId, "IX_StatPlayerOnMap_TeamId");

                entity.Property(e => e.Adr).HasColumnName("ADR");

                entity.HasOne(d => d.MapOfGame)
                    .WithMany(p => p.StatPlayerOnMaps)
                    .HasForeignKey(d => d.MapOfGameId);

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.StatPlayerOnMaps)
                    .HasForeignKey(d => d.PlayerId);

            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.TeamName).HasMaxLength(30);

                entity.Property(e => e.TeamNationality).HasMaxLength(30);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Role).IsRequired();

                entity.Property(e => e.Username).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
