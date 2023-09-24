using DadJokeBotLibrary.Model;

namespace DadJokeBotLibrary
{
    public interface IJokeClient
    {
        public Task<RandomDadJoke> GetRandomDadJoke();

        public Task<SearchedDadJokes> GetSearchedJoke(string? searchString);

    }
}
