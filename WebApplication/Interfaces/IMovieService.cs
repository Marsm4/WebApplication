using Microsoft.AspNetCore.Mvc;
using WebApplication1;

namespace MyWebApp.Interfaces
{
    public interface IMovieService
    {
        Task<ActionResult<IEnumerable<Movie>>> GetMoviesAsync();
        Task<ActionResult<Movie>> GetMovieByIdAsync(int id);
        Task<ActionResult<Movie>> AddMovieAsync(Movie movie);
        Task<IActionResult> UpdateMovieAsync(int id, Movie movie);
        Task<IActionResult> DeleteMovieAsync(int id);
    }
}
