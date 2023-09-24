using System.Text.RegularExpressions;

namespace DadJokeBotLibrary
{
    /// <summary>
    /// Helper class provides functions to format jokes
    /// </summary>
    public static class Helper
    {
        public static string? GroupByLength(string joke)
        {
            string? length = string.Empty;
            int numberOfWords = joke.Split(' ').Length;

            if (numberOfWords < 10)
            {
                length = Enum.GetName(JokeTypeEnum.Short);
            }
            else if (numberOfWords < 20)
            {
                length = Enum.GetName(JokeTypeEnum.Medium);
            }
            else if (numberOfWords >= 20)
            {
                length = Enum.GetName(JokeTypeEnum.Long);
            }
            return length;
        }

        public static string FormatJoke(string joke, string? searchText) {
            string formattedJoke = string.Empty;

            if (!string.IsNullOrWhiteSpace(searchText))
            {                
                formattedJoke = Regex.Replace(joke, searchText, $"<{searchText.ToUpper()}>", RegexOptions.IgnoreCase);
            }
            else
            {
                formattedJoke = joke;
            }            

            return formattedJoke;
        }

    }
}
