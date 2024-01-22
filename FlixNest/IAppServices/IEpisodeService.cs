using FlixNest.Models;

namespace FlixNest.IAppServices
{
    public interface IEpisodeService
    {
        public List<Episode> GetAllEpisodes();
        public void CreateEpisode(Episode episode, Movie movie);

        public void UpdateEpisode(Episode episode, Movie movie);
        public void DeleteEpisode(int id);
        public List<Episode> getEpisodeDelete();
        public void RestoreEpisode(Episode episode);
        public void DeleteCompleteEpisode(int id);
        public Episode findById(int id);

        public List<Episode> GetEpisodeByMovieId(int movieId);
        public List<Episode> GetEpbyTime();
    }
}
