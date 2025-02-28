using Microsoft.AspNetCore.Mvc;
using MyWebApp.Interfaces;
using System.Threading.Tasks;
using WebApplication1;


[Route("api/movies")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
    {
        return await _movieService.GetMoviesAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
        return await _movieService.GetMovieByIdAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> PostMovie(Movie movie)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return await _movieService.AddMovieAsync(movie);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int id, Movie movie)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return await _movieService.UpdateMovieAsync(id, movie);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        return await _movieService.DeleteMovieAsync(id);
    }
}