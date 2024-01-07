using FlixNest.Models;
using Hangfire;

namespace FlixNest.Repository.EpisodeRepository
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private FlixNestDbContext _context;
        public EpisodeRepository(FlixNestDbContext context)
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
        public bool CreateEpisode(Episode episode, Movie movie)
        {
            _context.episodes.Add(episode);
            _context.SaveChanges();

            Eplog(episode, "Đã được thêm");
            BackgroundJob.Enqueue(() => SuccessfulCreation(episode.EpisodeId, episode.EpisodeName,movie.MovieName, "Tạo thành công"));

            return true;
        }

        public bool DeleteEpisode(int id)
        {
            Episode episode = _context.episodes.FirstOrDefault(x => x.EpisodeId == id);
            if (episode != null)
            {
                episode.IsDeleted = true;
                Eplog(episode, "Đã được xóa tạm thời");
                BackgroundJob.Enqueue(() => SuccessfulDeleted(episode.EpisodeId, episode.EpisodeName, episode.MovieId, "Xóa thành công"));

                _context.SaveChanges();
            }

            return true;
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

        public bool UpdateEpisode(Episode episode, Movie movie)
        {
            Episode ep = _context.episodes.FirstOrDefault(x => x.EpisodeId == episode.EpisodeId);
            if (episode != null)
            {
                ep.EpisodeName = episode.EpisodeName;
                ep.Video = episode.Video;
                ep.ReleaseDate = episode.ReleaseDate;
                _context.SaveChanges();
                Eplog(episode, "Đã được cập nhật");
                BackgroundJob.Enqueue(() => SuccessfulUpdate(episode.EpisodeId, episode.EpisodeName, movie.MovieName, "Cập nhật thành công"));

            }
            return true;
        }

        public List<Episode> getEpisodeDelete()
        {
            return _context.episodes.Where(x => x.IsDeleted).ToList();
        }

        public bool RestoreEpisode(Episode episode)
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
            return true;
        }

        public bool DeleteCompleteEpisode(int id)
        {
            Episode episode = _context.episodes.FirstOrDefault(x => x.EpisodeId == id);
                _context.episodes.Remove(episode);
                _context.SaveChanges();
            return true;
        }

        public void SuccessfulCreation(int id, string name,string moviename, string des)
        {
            Episode createEpisode = _context.episodes.FirstOrDefault(x => x.EpisodeId == id);
        }
        public void SuccessfulUpdate(int episodeId, string name, string movieName,string des)
        {
            Episode updateGenre = findById(episodeId);
        }
        public void SuccessfulDeleted(int episodeId, string name,int id, string des)
        {
            Episode DeleteGenre = findById(episodeId);
        }
    }
}
