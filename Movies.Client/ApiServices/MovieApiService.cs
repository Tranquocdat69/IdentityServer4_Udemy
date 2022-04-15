using IdentityModel.Client;
using Movies.Client.Models;
using Newtonsoft.Json;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
        private readonly HttpClient _httpClient;

        public MovieApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MovieAPIClient") ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public Task<Movie> CreateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovie(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovie(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/movies");
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(content);

            return movies;

            /*var apiClientCredentitals = new ClientCredentialsTokenRequest()
            {
                Address = "https://localhost:7086/connect/token",
                ClientId = "movieClient",
                ClientSecret = "secret",
                Scope = "movieAPI"
            };
            DiscoveryDocumentResponse discoveryDocumentResponse = await _httpClient.GetDiscoveryDocumentAsync("https://localhost:7086");
            if (discoveryDocumentResponse.IsError)
            {
                return null;
            }
            TokenResponse tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(apiClientCredentitals);
            if (tokenResponse.IsError)
            {
                return null;
            }
            _httpClient.SetBearerToken(tokenResponse.AccessToken);
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("https://localhost:7025/api/movies");
            httpResponseMessage.EnsureSuccessStatusCode();

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(content);

            return movies;*/
            /* var movieList = new List<Movie>() { 
                 new Movie()
                 {
                     Id = 1,
                     Genre = "Comics",
                     ImageUrl = "imageurl",
                     Owner = "dattq",
                     Rating = "9.6",
                     ReleaseDate = DateTime.Now,
                     Title = "OK"
                 }
             };

             return await Task.FromResult(movieList);*/
        }

        public Task<Movie> UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
