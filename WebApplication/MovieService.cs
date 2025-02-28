using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Interfaces;
using WebApplication1;

namespace BlazorMovieApp.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<ActionResult<Movie>> GetMovieByIdAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return new NotFoundResult();
            }
            return movie;
        }

        public async Task<ActionResult<Movie>> AddMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return new CreatedAtActionResult("GetMovie", "Movies", new { id = movie.Id }, movie);
        }

        public async Task<IActionResult> UpdateMovieAsync(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return new BadRequestResult();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Movies.Any(e => e.Id == id))
                {
                    return new NotFoundResult();
                }
                else
                {
                    throw;
                }
            }

            return new NoContentResult();
        }//hj

        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return new NotFoundResult();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}