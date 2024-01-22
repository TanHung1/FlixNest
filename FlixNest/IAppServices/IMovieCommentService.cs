namespace FlixNest.IAppServices
{
    public interface IMovieCommentService
    {
        public void UserComment(string userId, int MovieId, string title);
    }
}
