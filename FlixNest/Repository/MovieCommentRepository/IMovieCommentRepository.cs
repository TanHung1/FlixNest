namespace FlixNest.Repository.MovieCommentRepository
{
    public interface IMovieCommentRepository
    {
        public bool UserComment(string userId, int MovieId, string title);
    }
}
