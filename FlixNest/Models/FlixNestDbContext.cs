using Microsoft.EntityFrameworkCore;
namespace FlixNest.Models
{
    public class FlixNestDbContext : DbContext
    {
        public FlixNestDbContext(DbContextOptions<FlixNestDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<MovieActor>()
            //    .HasKey(bc => new { bc.MovieId, bc.ActId });
            modelBuilder.Entity<MovieActor>()
     .HasKey(ma => ma.MovieActorId);
            modelBuilder.Entity<MovieActor>()
                .HasOne(bc => bc.Movie)
                .WithMany(m => m.movieActors)
                .HasForeignKey(bc => bc.MovieId);
            modelBuilder.Entity<MovieActor>()
                .HasOne(bc => bc.Actor)
                .WithMany(m => m.movieActors)
                .HasForeignKey(bc => bc.ActId);

            //modelBuilder.Entity<MovieDirector>()
            //    .HasKey(dc => new { dc.MovieId, dc.DirId });
            modelBuilder.Entity<MovieDirector>()
   .HasKey(ma => ma.MovieDirectorId);
            modelBuilder.Entity<MovieDirector>()
                .HasOne(dc => dc.Movie)
                .WithMany(dc => dc.movieDirectors)
                .HasForeignKey(dc => dc.MovieId);
            modelBuilder.Entity<MovieDirector>()
                .HasOne(dc => dc.Director)
                .WithMany(dc => dc.movieDirectors)
                .HasForeignKey(dc => dc.DirId);

            //modelBuilder.Entity<MovieGenre>()
            //    .HasKey(mg => new { mg.MovieId, mg.GenreId });
            modelBuilder.Entity<MovieGenre>()
   .HasKey(ma => ma.MovieGenreId);
            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(mg => mg.movieGenres)
                .HasForeignKey(mg => mg.MovieId);
            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(mg => mg.movieGenres)
                .HasForeignKey(mg => mg.GenreId);

            // Mối quan hệ giữa bảng Movie và EPISODE
            modelBuilder.Entity<Episode>()
                .HasOne(ep => ep.Movie)
                .WithMany(ep => ep.episodes)
                .HasForeignKey(ep => ep.MovieId);

            //modelBuilder.Entity<MovieFollow>()
            //    .HasKey(mf => new { mf.UserId, mf.MovieId });
            //         modelBuilder.Entity<MovieFollow>()
            //.HasKey(ma => ma.MovieFollowId);

            //         modelBuilder.Entity<MovieFollow>()
            //             .HasOne(mf => mf.User)
            //             .WithMany(u => u.MovieFollows)
            //             .HasForeignKey(mf => mf.UserId);

            //         modelBuilder.Entity<MovieFollow>()
            //             .HasOne(mf => mf.Movie)
            //             .WithMany(m => m.MovieFollows)
            //             .HasForeignKey(mf => mf.MovieId);

            base.OnModelCreating(modelBuilder); // Gọi cuối cùng để áp dụng cấu hình mặc định
        }


        public DbSet<Actor> Actor { get; set; }

        public DbSet<Director> Director { get; set; }

        public DbSet<Episode> episodes { get; set; }
        public DbSet<Movie> Movie { get; set; }

        public DbSet<Genre> Genre { get; set; }

        public DbSet<MovieActor> MovieActor { get; set; }

        public DbSet<MovieDirector> MovieDirector { get; set; }

        public DbSet<MovieGenre> MovieGenre { get; set; }

        public DbSet<Year> Years { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<MovieFollow> MovieFollows { get; set; }

        public DbSet<MovieComment> MovieComments { get; set; }


        public DbSet<MovieActivity> MovieActivity { get; set; }

        public DbSet<EpisodeActivity> EpisodeActivity { get; set; }
    }
}
