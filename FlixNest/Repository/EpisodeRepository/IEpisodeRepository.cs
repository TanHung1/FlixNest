using FlixNest.Models;

namespace FlixNest.Repository.EpisodeRepository
{
    public interface IEpisodeRepository
    {
        public List<Episode> GetAllEpisodes();
        public bool CreateEpisode(Episode episode, Movie movie);

        public bool UpdateEpisode(Episode episode, Movie movie);
        public bool DeleteEpisode(int id);
        public List<Episode> getEpisodeDelete();
        public bool RestoreEpisode(Episode episode);
        public bool DeleteCompleteEpisode(int id);
        public Episode findById(int id);

        public List<Episode> GetEpisodeByMovieId(int movieId);
        public List<Episode> GetEpbyTime();
    }
}
