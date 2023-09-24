namespace DadJokeBotLibrary
{
    /// <summary>
    /// Contract to fetch jokes
    /// </summary>
    public interface IJokeGenerator
    {
        /// <summary>
        /// Fetches random jokes
        /// </summary>
        /// <returns>joke as string</returns>
        string? GetRandomJoke();

        /// <summary>
        /// Fetches jokes based on the query string
        /// </summary>
        /// <param name="searchString">Query string</param>
        /// <returns>Grouped list of jokes based on length</returns>
        List<IGrouping<string?, string>> GetSearchedJokes(string? searchString);
    }
}
