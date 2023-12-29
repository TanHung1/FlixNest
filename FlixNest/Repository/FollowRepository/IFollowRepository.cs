namespace FlixNest.Repository.FollowRepository
{
    public interface IFollowRepository
    {
        public bool FollowMovie(string userId, int Movieid);
        public bool IsFollowingMovie(string userId, int movieId);
    }
}
