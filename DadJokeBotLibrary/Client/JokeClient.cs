using DadJokeBotLibrary.Model;
using DadJokeBotLibrary.Utilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DadJokeBotLibrary
{
    /// <summary>
    /// Fetches the Jokes from the WebAPI and returns the results.
    /// </summary>
    public class JokeClient : IDisposable, IJokeClient
    {
        private readonly HttpClient _httpClient;
        

        /// <summary>
        /// Creates an instance of <see cref="JokeClient"/> and initializes the HTTP client instance.
        /// </summary>
        public JokeClient() {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://icanhazdadjoke.com/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Fetches a Random joke from the API, implementation of interface.
        /// Note: async decleration is not allowed in interface.
        /// </summary>
        /// <returns>Instance of <see cref="RandomDadJoke"/>, null if not successful</returns>
        public Task<RandomDadJoke> GetRandomDadJoke()
        {
            return GetRandomJoke();
        }

        /// <summary>
        /// Fetches jokes matching the querystring,implementation of interface.
        /// Note: async decleration is not allowed in interface.
        /// </summary>
        /// <param name="searchString">query string</param>
        /// <returns>Instance of <see cref="SearchedDadJokes"/>, null if not successful</returns>
        public Task<SearchedDadJokes> GetSearchedJoke(string? searchString)
        {
            return GetSearchedDadJoke(searchString);
        }

        /// <summary>
        /// Fetches a Random joke from the API
        /// </summary>
        /// <returns>Instance of <see cref="RandomDadJoke"/> null if not successful</returns>
        private async Task<RandomDadJoke> GetRandomJoke()
        {            
            try
            {
                using (var response = await _httpClient.GetAsync("").ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        return JsonConvert.DeserializeObject<RandomDadJoke>(res);
                    }
                }
            } catch (Exception ex)
            {                
                FileLogger.Log($"Exception message: {ex.Message}. Stack trace {ex.StackTrace}");
            }

            return null;
        }

        /// <summary>
        /// Fetches jokes matching the querystring
        /// </summary>
        /// <param name="searchString">query string</param>
        /// <returns>Instance of <see cref="SearchedDadJokes"/>, null if not successful</returns>
        private async Task<SearchedDadJokes> GetSearchedDadJoke(string? searchString)
        {
            try
            {
                var searchUrl = $"search?term={searchString}&limit=30";
                using (var response = await _httpClient.GetAsync(searchUrl).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        return JsonConvert.DeserializeObject<SearchedDadJokes>(res);
                    }
                }
            } catch(Exception ex)
            {
                FileLogger.Log($"Exception message: {ex.Message}. Stack trace {ex.StackTrace}");
            }
            return null;
        }

        /// <summary>
        /// IDisposable Implementation
        /// </summary>
        public void Dispose()
        {
            _httpClient?.Dispose();
        }

    }
}