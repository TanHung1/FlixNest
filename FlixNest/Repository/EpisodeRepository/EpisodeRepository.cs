using FlixNest.Models;

namespace FlixNest.Repository.EpisodeRepository
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private FlixNestDbContext _context;
        public EpisodeRepository(FlixNestDbContext context)
        {
            _context = context;
        }
        public bool CreateEpisode(Episode episode)
        {
            _context.episodes.Add(episode);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteEpisode(int id)
        {
            Episode episode = _context.episodes.FirstOrDefault(x => x.EpisodeId == id);
            _context.episodes.Remove(episode);
            _context.SaveChanges();
            return true;
        }

        public Episode findById(int id)
        {
            Episode episode = _context.episodes.FirstOrDefault(x => x.EpisodeId == id);
            return episode;
        }

        public List<Episode> GetAllEpisodes()
        {
            return _context.episodes.ToList();
        }

        public List<Episode> GetEpbyTime()
        {
            return _context.episodes.OrderByDescending(x => x.ReleaseDate).ToList();
        }

        public List<Episode> GetEpisodeByMovieId(int movieId)
        {
            return _context.episodes.Where(x => x.MovieId == movieId).ToList();
        }

        public bool UpdateEpisode(Episode episode)
        {
            Episode ep = _context.episodes.FirstOrDefault(x => x.EpisodeId == episode.EpisodeId);
            if (episode != null)
            {
                ep.EpisodeName = episode.EpisodeName;
                ep.Video = episode.Video;
                ep.ReleaseDate = episode.ReleaseDate;
                _context.SaveChanges();

            }
            return true;
        }
    }
}
