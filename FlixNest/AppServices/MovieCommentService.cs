﻿using FlixNest.Areas.Identity.Data;
using FlixNest.IAppServices;
using FlixNest.Models;
using Microsoft.AspNetCore.Identity;

namespace FlixNest.AppServices
{
    public class MovieCommentService : IMovieCommentService
    {
        private FlixNestDbContext _context;
        private UserManager<AccountUser> _userManager;
        public MovieCommentService(FlixNestDbContext context, UserManager<AccountUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void UserComment(string userId, int MovieId, string title)
        {
            Guid userComment = Guid.Parse(userId);
            var movieCommnet = new MovieComment
            {
                UserId = userComment,
                MovieId = MovieId,
                Title = title
            };
            _context.MovieComments.Add(movieCommnet);
            _context.SaveChanges();


        }

    }
}