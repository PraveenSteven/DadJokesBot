using DadJokeBotLibrary;
using DadJokeBotLibrary.Model;
using Moq;

namespace TestDadJokeBot
{
    public class JokeGeneratorTest
    {
        JokeGenerator service;
        Mock<IJokeClient> _client;

        [SetUp]
        public void Setup()
        {
            _client = new Mock<IJokeClient>();
            service = new JokeGenerator(_client.Object);
        }

        [Test]        
        public void TestGetRandomJoke()
        {
            // Arrange
            var actualJoke = "What do you give a sick lemon? Lemonaid.";
            _client.Setup(x => x.GetRandomDadJoke()).Returns(Task.FromResult(new RandomDadJoke() { Joke= actualJoke }));
            
            // Act
            var randomJoke = service.GetRandomJoke();

            //Assert
            Assert.IsNotNull(randomJoke);
            Assert.IsTrue(randomJoke?.Equals(actualJoke));
        }

        [Test]
        public void TestGetRandomDadJokeForNull()
        {
            // Arrange
            _client.Setup(x => x.GetRandomDadJoke()).Returns(Task.FromResult<RandomDadJoke>(null));

            // Act
            var randomJoke = service.GetRandomJoke();

            //Assert
            Assert.IsNull(randomJoke);
        }

        [Test]
        [TestCase("Hipster")]
        public void TestGetSearchedJokeswithQueryString(string searchedString)
        {
            // Arrange
            List<RandomDadJoke> searchedJokes = new List<RandomDadJoke> {
                new RandomDadJoke{ Joke = "How much does a hipster weigh? An instagram." },
                new RandomDadJoke{ Joke ="How did the hipster burn the roof of his mouth? He ate the pizza before it was cool." },
                new RandomDadJoke{ Joke ="How many hipsters does it take to change a lightbulb? Oh, it's a really obscure number. You've probably never heard of it." }
            };
            var result = new SearchedDadJokes { Results = searchedJokes };
            _client.Setup(x => x.GetSearchedJoke(searchedString)).Returns(Task.FromResult<SearchedDadJokes>(result));

            // Act
            var jokeResult = service.GetSearchedJokes(searchedString);

            //Assert
            Assert.IsTrue(jokeResult.Any());

            Assert.IsTrue(jokeResult.Count == 3);

            Assert.IsTrue(jokeResult.Any(x => JokeTypeEnum.Short.ToString().Equals(x.Key)));

            Assert.IsTrue(jokeResult.Any(x => JokeTypeEnum.Medium.ToString().Equals(x.Key)));

            Assert.IsTrue(jokeResult.Any(x => JokeTypeEnum.Long.ToString().Equals(x.Key)));

            foreach(var joke in jokeResult)
            {
                var jokes = joke.ToList();
                Assert.IsTrue(jokes.Any(j => j.Contains($"<{searchedString.ToUpper()}>")));
            }

        }

        [Test]
        public void TestGetSearchedJokesForNull()
        {
            //Arrange
            _client.Setup(x => x.GetSearchedJoke(It.IsAny<string>())).Returns(Task.FromResult<SearchedDadJokes>(null));

            // Act
            var jokeResult = service.GetSearchedJokes(It.IsAny<string>());

            // Assert
            Assert.IsNotNull(jokeResult);
            Assert.IsFalse(jokeResult.Any());
        }
    }
}