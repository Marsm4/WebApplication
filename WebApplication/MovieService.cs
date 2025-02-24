
using System.Net.Http.Json;
using WebApplication1;

namespace BlazorMovieApp.Services
{
    public class MovieService
    {
        private readonly HttpClient _httpClient;

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Movie>>("api/movies");
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Movie>($"api/movies/{id}");
        }

        public async Task AddMovieAsync(Movie movie)
        {
            var response = await _httpClient.PostAsJsonAsync("api/movies", movie);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/movies/{movie.Id}", movie);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteMovieAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/movies/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка при удалении фильма: {ex.Message}");
            }
        }
    }
}
