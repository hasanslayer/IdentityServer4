using IdentityModel.Client;
using Movies.Client.Models;
using Newtonsoft.Json;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
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
            // 1- get token from identity server 
            // 2- send request to protected API
            // 3- deserialize object to MovieList

            // 1. get our api credentials. This must be registered on Identity Server
            var apiCredentials = new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:5005/connect/token",

                ClientId = "movieClient",
                ClientSecret = "secret",


                // this is the scope our protected API requires.
                Scope = "movieAPI"
            };

            // create a new httpclient to contact to our IdentityServer (localhost:5005)
            var client = new HttpClient();

            // just check if we can reach the discovery document.
            var dicov = await client.GetDiscoveryDocumentAsync("https://localhost:5005");
            if (dicov.IsError)
            {
                return null; // throw 500 error
            }

            // 2. authenticate and get access token from identity server
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(apiCredentials);


            // 2 - send request to protected api

            // httpclient to contact with protected api
            var apiClient = new HttpClient();

            // 3. set access token in the request authorization: Bearer <token>
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            // 4. send a request to our protected api
            var response = await apiClient.GetAsync("https://localhost:5001/api/movies");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            // deserialize object to movieList
            var movieList = JsonConvert.DeserializeObject<List<Movie>>(content);

            return movieList;
        }

        public Task<Movie> UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
