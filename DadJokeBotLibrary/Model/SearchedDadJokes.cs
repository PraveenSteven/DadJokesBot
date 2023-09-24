namespace DadJokeBotLibrary.Model
{
    public class SearchedDadJokes
    {
        public int Limit { get; set; }
        public List<RandomDadJoke> Results { get; set; } = new List<RandomDadJoke>();
        public string Search_Term { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int Total_Jokes { get; set; }
    }
}
