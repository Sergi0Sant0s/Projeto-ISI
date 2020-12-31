﻿// <auto-generated />
using System;
using DATA.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DATA.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("DATA.Entities.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateOfEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("EventId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            EventId = 1,
                            DateOfEnd = new DateTime(2021, 1, 5, 1, 29, 38, 248, DateTimeKind.Local).AddTicks(1300),
                            DateOfStart = new DateTime(2020, 12, 31, 1, 29, 38, 245, DateTimeKind.Local).AddTicks(1367),
                            EventName = "Blast New York"
                        });
                });

            modelBuilder.Entity("DATA.Entities.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<DateTime>("GameDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TeamAId")
                        .HasColumnType("int");

                    b.Property<int>("TeamBId")
                        .HasColumnType("int");

                    b.Property<int?>("TeamWinnerId")
                        .HasColumnType("int");

                    b.HasKey("GameId");

                    b.HasIndex("EventId");

                    b.HasIndex("TeamAId");

                    b.HasIndex("TeamBId");

                    b.HasIndex("TeamWinnerId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("DATA.Entities.Map", b =>
                {
                    b.Property<int>("MapId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("MapId");

                    b.ToTable("Maps");

                    b.HasData(
                        new
                        {
                            MapId = 1,
                            Description = "Inferno"
                        },
                        new
                        {
                            MapId = 2,
                            Description = "Vertigo"
                        },
                        new
                        {
                            MapId = 3,
                            Description = "Dust2"
                        },
                        new
                        {
                            MapId = 4,
                            Description = "Overpass"
                        },
                        new
                        {
                            MapId = 5,
                            Description = "Train"
                        });
                });

            modelBuilder.Entity("DATA.Entities.MapOfGame", b =>
                {
                    b.Property<int>("MapOfGameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("MapaId")
                        .HasColumnType("int");

                    b.Property<int?>("TeamAResult")
                        .HasColumnType("int");

                    b.Property<int?>("TeamBResult")
                        .HasColumnType("int");

                    b.HasKey("MapOfGameId");

                    b.HasIndex("GameId");

                    b.HasIndex("MapaId");

                    b.ToTable("MapOfGame");
                });

            modelBuilder.Entity("DATA.Entities.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Facebook")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instagram")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<string>("Twitter")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            PlayerId = 1,
                            Age = 29,
                            Name = "Gabriel Toledo",
                            Nationality = "Brazil",
                            Nickname = "FalleN"
                        },
                        new
                        {
                            PlayerId = 2,
                            Age = 28,
                            Name = "Vito Giuseppe",
                            Nationality = "Brazil",
                            Nickname = "kNgV-"
                        },
                        new
                        {
                            PlayerId = 3,
                            Age = 25,
                            Name = "Nicolai Reedtz",
                            Nationality = "Dinamarca",
                            Nickname = "device"
                        },
                        new
                        {
                            PlayerId = 4,
                            Age = 27,
                            Name = "Peter Rasmussen",
                            Nationality = "Dinamarca",
                            Nickname = "dupreeh"
                        },
                        new
                        {
                            PlayerId = 5,
                            Age = 25,
                            Name = "Lukas Rossander",
                            Nationality = "Dinamarca",
                            Nickname = "gla1ve"
                        });
                });

            modelBuilder.Entity("DATA.Entities.StatPlayerOnMap", b =>
                {
                    b.Property<int>("StatPlayerOnMapId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double>("ADR")
                        .HasColumnType("float");

                    b.Property<int>("Deaths")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("Kills")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("StatPlayerOnMapId");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("StatPlayerOnMap");
                });

            modelBuilder.Entity("DATA.Entities.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("TeamName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("TeamNationality")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("TeamRanking")
                        .HasColumnType("int");

                    b.HasKey("TeamId");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            TeamId = 1,
                            TeamName = "Mibr",
                            TeamNationality = "Brazil",
                            TeamRanking = 21
                        },
                        new
                        {
                            TeamId = 2,
                            TeamName = "Astralis",
                            TeamNationality = "Dinamarca",
                            TeamRanking = 1
                        },
                        new
                        {
                            TeamId = 3,
                            TeamName = "Natus Vincere",
                            TeamNationality = "Russia",
                            TeamRanking = 3
                        },
                        new
                        {
                            TeamId = 4,
                            TeamName = "OG",
                            TeamNationality = "Europa",
                            TeamRanking = 7
                        },
                        new
                        {
                            TeamId = 5,
                            TeamName = "Mousesports",
                            TeamNationality = "Europa",
                            TeamRanking = 9
                        });
                });

            modelBuilder.Entity("DATA.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "qwerty@qwerty.qwerty",
                            Password = "Admin",
                            Role = "Admin",
                            Username = "Admin"
                        },
                        new
                        {
                            UserId = 2,
                            Email = "qwerty@qwerty.qwerty",
                            Password = "User",
                            Role = "User",
                            Username = "User"
                        });
                });

            modelBuilder.Entity("EventTeam", b =>
                {
                    b.Property<int>("EventsEventId")
                        .HasColumnType("int");

                    b.Property<int>("TeamsTeamId")
                        .HasColumnType("int");

                    b.HasKey("EventsEventId", "TeamsTeamId");

                    b.HasIndex("TeamsTeamId");

                    b.ToTable("EventTeam");
                });

            modelBuilder.Entity("DATA.Entities.Game", b =>
                {
                    b.HasOne("DATA.Entities.Event", "Event")
                        .WithMany("Games")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATA.Entities.Team", "TeamA")
                        .WithMany("GamesTeamA")
                        .HasForeignKey("TeamAId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATA.Entities.Team", "TeamB")
                        .WithMany("GamesTeamB")
                        .HasForeignKey("TeamBId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATA.Entities.Team", "TeamWinner")
                        .WithMany("GamesTeamWinner")
                        .HasForeignKey("TeamWinnerId");

                    b.Navigation("Event");

                    b.Navigation("TeamA");

                    b.Navigation("TeamB");

                    b.Navigation("TeamWinner");
                });

            modelBuilder.Entity("DATA.Entities.MapOfGame", b =>
                {
                    b.HasOne("DATA.Entities.Game", "Game")
                        .WithMany("MapOfGame")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATA.Entities.Map", "Mapa")
                        .WithMany("MapofGame")
                        .HasForeignKey("MapaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Mapa");
                });

            modelBuilder.Entity("DATA.Entities.Player", b =>
                {
                    b.HasOne("DATA.Entities.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("DATA.Entities.StatPlayerOnMap", b =>
                {
                    b.HasOne("DATA.Entities.Game", "Game")
                        .WithMany("StatPlayerOnMap")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATA.Entities.Player", "Player")
                        .WithMany("StatPlayerOnMap")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATA.Entities.Team", "Team")
                        .WithMany("StatPlayerOnMap")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Player");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("EventTeam", b =>
                {
                    b.HasOne("DATA.Entities.Event", null)
                        .WithMany()
                        .HasForeignKey("EventsEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATA.Entities.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamsTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DATA.Entities.Event", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("DATA.Entities.Game", b =>
                {
                    b.Navigation("MapOfGame");

                    b.Navigation("StatPlayerOnMap");
                });

            modelBuilder.Entity("DATA.Entities.Map", b =>
                {
                    b.Navigation("MapofGame");
                });

            modelBuilder.Entity("DATA.Entities.Player", b =>
                {
                    b.Navigation("StatPlayerOnMap");
                });

            modelBuilder.Entity("DATA.Entities.Team", b =>
                {
                    b.Navigation("GamesTeamA");

                    b.Navigation("GamesTeamB");

                    b.Navigation("GamesTeamWinner");

                    b.Navigation("Players");

                    b.Navigation("StatPlayerOnMap");
                });
#pragma warning restore 612, 618
        }
    }
}
