namespace FlixNest.Repository.MovieCommentRepository
{
    public interface IMovieCommentRepository
    {
        public void UserComment(string userId, int MovieId, string title);
    }
}
