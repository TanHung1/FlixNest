using FlixNest.Models;

namespace FlixNest.Repository.EpisodeRepository
{
    public interface IEpisodeRepository
    {
        public List<Episode> GetAllEpisodes();
        public bool CreateEpisode(Episode episode);

        public bool UpdateEpisode(Episode episode);
        public bool DeleteEpisode(int id);

        public Episode findById(int id);

        public List<Episode> GetEpisodeByMovieId(int movieId);
        public List<Episode> GetEpbyTime();

    }
}
