﻿using FlixNest.IAppServices;
using FlixNest.Models;

namespace FlixNest.AppServices
{
    public class MovieActorService : IMovieActorService
    {
        private FlixNestDbContext _context;
        public MovieActorService(FlixNestDbContext context)
        {
            _context = context;
        }
        public void createMovieActor(Movie movie, List<int> ActorList)
        {
            foreach (var actorId in ActorList)
            {
                MovieActor movieactor = new MovieActor()
                {
                    ActId = actorId,
                    MovieId = movie.MovieId,
                    Actor = _context.Actor.Find(actorId),
                    Movie = movie
                };

                _context.MovieActor.Add(movieactor);
                _context.SaveChanges();
            }

        }



        public void updateMovieActor(Movie movie, List<int> ActorList)
        {
            //lấy danh sách hiện tại của phim
            var existactor = _context.MovieActor.Where(x => x.MovieId == movie.MovieId).ToList();
            //xóa những thứ không dc chọn
            foreach (var movieActor in existactor)
            {
                if (!ActorList.Contains(movieActor.ActId))
                {
                    _context.MovieActor.Remove(movieActor);
                }

            }
            foreach (var actorId in ActorList)
            {
                if (!existactor.Any(x => x.ActId == actorId))
                {
                    _context.MovieActor.Add(new MovieActor { ActId = actorId, MovieId = movie.MovieId });
                }
            }

            _context.SaveChanges();

        }
    }
}