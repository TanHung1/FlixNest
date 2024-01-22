namespace FlixNest.IAppServices
{
    public interface IFollowService
    {
        public bool FollowMovie(string userId, int Movieid);
        public bool IsFollowingMovie(string userId, int movieId);
    }
}
