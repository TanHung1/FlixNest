﻿// <auto-generated />
using System;
using FlixNest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlixNest.Migrations
{
    [DbContext(typeof(FlixNestDbContext))]
    [Migration("20231220061205_tableyear")]
    partial class tableyear
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FlixNest.Models.Actor", b =>
                {
                    b.Property<int>("ActId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActId"), 1L, 1);

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActId");

                    b.ToTable("Actor");
                });

            modelBuilder.Entity("FlixNest.Models.Director", b =>
                {
                    b.Property<int>("DirId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DirId"), 1L, 1);

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DirId");

                    b.ToTable("Director");
                });

            modelBuilder.Entity("FlixNest.Models.Episode", b =>
                {
                    b.Property<int>("EpisodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EpisodeId"), 1L, 1);

                    b.Property<string>("EpisodeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Video")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EpisodeId");

                    b.HasIndex("MovieId");

                    b.ToTable("episodes");
                });

            modelBuilder.Entity("FlixNest.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"), 1L, 1);

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreId");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("FlixNest.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MovieId"), 1L, 1);

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MovieCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MovieName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MovieTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MovieTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MovieYear")
                        .HasColumnType("int");

                    b.Property<string>("VideoPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MovieId");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("FlixNest.Models.MovieActor", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("ActId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "ActId");

                    b.HasIndex("ActId");

                    b.ToTable("MovieActor");
                });

            modelBuilder.Entity("FlixNest.Models.MovieDirector", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("DirId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "DirId");

                    b.HasIndex("DirId");

                    b.ToTable("MovieDirector");
                });

            modelBuilder.Entity("FlixNest.Models.MovieGenre", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("MovieGenre");
                });

            modelBuilder.Entity("FlixNest.Models.Year", b =>
                {
                    b.Property<int>("YearId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("YearId"), 1L, 1);

                    b.Property<string>("YearName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("YearId");

                    b.ToTable("Years");
                });

            modelBuilder.Entity("FlixNest.Models.Episode", b =>
                {
                    b.HasOne("FlixNest.Models.Movie", "Movie")
                        .WithMany("episodes")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("FlixNest.Models.MovieActor", b =>
                {
                    b.HasOne("FlixNest.Models.Actor", "Actor")
                        .WithMany("movieActors")
                        .HasForeignKey("ActId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlixNest.Models.Movie", "Movie")
                        .WithMany("movieActors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("FlixNest.Models.MovieDirector", b =>
                {
                    b.HasOne("FlixNest.Models.Director", "Director")
                        .WithMany("movieDirectors")
                        .HasForeignKey("DirId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlixNest.Models.Movie", "Movie")
                        .WithMany("movieDirectors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Director");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("FlixNest.Models.MovieGenre", b =>
                {
                    b.HasOne("FlixNest.Models.Genre", "Genre")
                        .WithMany("movieGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlixNest.Models.Movie", "Movie")
                        .WithMany("movieGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("FlixNest.Models.Actor", b =>
                {
                    b.Navigation("movieActors");
                });

            modelBuilder.Entity("FlixNest.Models.Director", b =>
                {
                    b.Navigation("movieDirectors");
                });

            modelBuilder.Entity("FlixNest.Models.Genre", b =>
                {
                    b.Navigation("movieGenres");
                });

            modelBuilder.Entity("FlixNest.Models.Movie", b =>
                {
                    b.Navigation("episodes");

                    b.Navigation("movieActors");

                    b.Navigation("movieDirectors");

                    b.Navigation("movieGenres");
                });
#pragma warning restore 612, 618
        }
    }
}
