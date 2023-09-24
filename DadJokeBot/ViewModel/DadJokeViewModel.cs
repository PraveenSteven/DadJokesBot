using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DadJokeBotLibrary;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace DadJokeBot
{
    public partial class DadJokeViewModel : ObservableObject
    {
        private IJokeGenerator _jokeGenerator;
        public ICommand? RandomJokeCommand { get; set; }

        public ICommand? SearchedJokeCommand { get; set; }

        [ObservableProperty]
        public string? _randomJoke;
        [ObservableProperty]
        public ICollectionView? _jokeListView;
        [ObservableProperty]
        public Visibility _showRandomJoke = Visibility.Collapsed;
        [ObservableProperty]
        public Visibility _showSearchedJoke = Visibility.Collapsed;
        [ObservableProperty]
        public Visibility _showerror = Visibility.Collapsed;

        public ObservableCollection<JokeDataModel> JokeModels { get; set; } = new ObservableCollection<JokeDataModel>();

        public DadJokeViewModel(IJokeGenerator jokeGenerator) { 
            _jokeGenerator = jokeGenerator;
            RandomJokeCommand = new RelayCommand(GetRandomJoke);
            SearchedJokeCommand = new RelayCommand<string?>(GetSearchedJoke);
        }

        private void GetRandomJoke()
        {
            RandomJoke = _jokeGenerator.GetRandomJoke();
            ShowRandomJoke = Visibility.Visible;
            ShowSearchedJoke = Visibility.Collapsed;
            Showerror = Visibility.Collapsed;
        }

        private void GetSearchedJoke(string? input)
        {
            JokeModels.Clear();
            var jokes = _jokeGenerator.GetSearchedJokes(input);
            foreach(var joke in jokes)
            {         
                JokeModels.Add(new JokeDataModel() { JokeType = joke.Key, Jokes = joke.ToList() });
            }
            JokeListView = CollectionViewSource.GetDefaultView(JokeModels);
            if (JokeModels.Any())
            {
                Showerror = Visibility.Collapsed;
                ShowRandomJoke = Visibility.Collapsed;
                ShowSearchedJoke = Visibility.Visible;
            } 
            else
            {
                Showerror = Visibility.Visible;
                ShowRandomJoke = Visibility.Collapsed;
                ShowSearchedJoke = Visibility.Collapsed;
            }
        }

    }
}
