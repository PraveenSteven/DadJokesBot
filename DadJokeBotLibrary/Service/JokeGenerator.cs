namespace DadJokeBotLibrary
{
    /// <summary>
    /// Service implementation to fetch and process jokes
    /// </summary>
    public class JokeGenerator : IJokeGenerator
    {
        private readonly IJokeClient _client;

        /// <summary>
        /// Creates an instance of <see cref="JokeGenerator"/>
        /// </summary>
        public JokeGenerator(IJokeClient jokeClient) { 
            _client = jokeClient;
        }

        /// <summary>
        /// Fetches jokes based on the query string and formats the jokes
        /// </summary>
        /// <param name="searchString">Query string</param>
        /// <returns>Grouped list of jokes based on length</returns>
        public List<IGrouping<string?,string>> GetSearchedJokes(string? searchString)
        {
            List<string> jokeList = new List<string>();
            var jokes = _client.GetSearchedJoke(searchString).Result;
            if (jokes != null) {
                jokes.Results.ForEach(j => jokeList.Add(Helper.FormatJoke(j.Joke, searchString)));
            }
            return jokeList.GroupBy(x => Helper.GroupByLength(x)).ToList();
        }

        /// <summary>
        /// Fetches random jokes
        /// </summary>
        /// <returns>joke as string</returns
        public string? GetRandomJoke()
        {
            var joke = _client.GetRandomDadJoke().Result;
            return joke?.Joke;
        }
    }
}
