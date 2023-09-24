using DadJokeBotLibrary;

namespace TestDadJokeBot
{
    [Explicit("This class interacts with the icanhazdadjoke API and needs to be run manually")]
    public class JokeClientTest
    {
        JokeClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new JokeClient();
        }

        [Test]
        public void TestGetRandomJoke()
        {
            // Act
            var joke = _client.GetRandomDadJoke();

            // Assert
            Assert.IsNotNull(joke);
            Assert.IsFalse(string.IsNullOrEmpty(joke.Result.Joke));

        }

        [Test]
        [TestCase("hipster")]
        [TestCase("Whiskey")]
        public void TestGetSearchedResultBasedOnQuery(string searchedString)
        {
            // Act
            var jokes = _client.GetSearchedJoke(searchedString);

            // Assert
            Assert.IsNotNull(jokes);

            Assert.IsTrue(jokes.Result.Results.Any());
        }

        [Test]
        [TestCase("abc")]
        public void TestGetSearchedResultBasedOnQueryWithNoJokes(string searchedString)
        {
            // Act
            var jokes = _client.GetSearchedJoke(searchedString);

            // Assert
            Assert.IsNotNull(jokes);

            Assert.IsFalse(jokes.Result.Results.Any());
        }
    }
}
