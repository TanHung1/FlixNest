using FlixNest.IAppServices;
using FlixNest.Models;
using Hangfire;

namespace FlixNest.AppServices
{
    public class EpisodeService : IEpisodeService
    {
        private FlixNestDbContext _context;
        public EpisodeService(FlixNestDbContext context)
        {
            _context = context;
        }

        private void Eplog(Episode episode, string Action)
        {
            Movie movie = _context.Movie.FirstOrDefault(X => X.MovieId == episode.MovieId);
            EpisodeActivity activity = new EpisodeActivity
            {
                EpisodeId = episode.EpisodeId,
                Action = Action,
                DateTime = DateTime.Now,
                Description = $"{Action} (Tập phim {episode.EpisodeName} của phim {movie?.MovieName})"

            };
            _context.EpisodeActivity.Add(activity);
            _context.SaveChanges();
        }
        public void CreateEpisode(Episode episode, Movie movie)
        {
            _context.episodes.Add(episode);
            _context.SaveChanges();

            Eplog(episode, "Đã được thêm");


        }

        public void DeleteEpisode(int id)
        {
            Episode episode = _context.episodes.FirstOrDefault(x => x.EpisodeId == id);
            if (episode != null)
            {
                episode.IsDeleted = true;
                Eplog(episode, "Đã được xóa tạm thời");

                _context.SaveChanges();
            }
        }

        public Episode findById(int id)
        {
            Episode episode = _context.episodes.FirstOrDefault(x => x.EpisodeId == id);
            return episode;
        }

        public List<Episode> GetAllEpisodes()
        {
            return _context.episodes.Where(x => !x.IsDeleted).ToList();
        }

        public List<Episode> GetEpbyTime()
        {
            return _context.episodes.OrderByDescending(x => x.ReleaseDate).ToList();
        }

        public List<Episode> GetEpisodeByMovieId(int movieId)
        {
            return _context.episodes.Where(x => x.MovieId == movieId && !x.IsDeleted).ToList();
        }

        public void UpdateEpisode(Episode episode, Movie movie)
        {
            Episode ep = _context.episodes.FirstOrDefault(x => x.EpisodeId == episode.EpisodeId);
            if (episode != null)
            {
                ep.EpisodeName = episode.EpisodeName;
                ep.Video = episode.Video;
                ep.ReleaseDate = episode.ReleaseDate;
                _context.SaveChanges();
                Eplog(episode, "Đã được cập nhật");

            }

        }

        public List<Episode> getEpisodeDelete()
        {
            return _context.episodes.Where(x => x.IsDeleted).ToList();
        }

        public void RestoreEpisode(Episode episode)
        {
            Episode ep = _context.episodes.FirstOrDefault(x => x.EpisodeId == episode.EpisodeId);
            if (episode != null)
            {
                ep.EpisodeName = episode.EpisodeName;
                ep.Video = episode.Video;
                ep.ReleaseDate = episode.ReleaseDate;
                ep.IsDeleted = false;
                _context.SaveChanges();
                Eplog(episode, "Đã được cập nhật");

            }

        }

        public void DeleteCompleteEpisode(int id)
        {
            Episode episode = _context.episodes.FirstOrDefault(x => x.EpisodeId == id);
            _context.episodes.Remove(episode);
            _context.SaveChanges();

        }


    }
}
