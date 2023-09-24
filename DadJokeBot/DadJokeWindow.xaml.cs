using System.Windows;
using System.Windows.Data;

namespace DadJokeBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DadJokeWindow : Window
    {
        public DadJokeWindow(DadJokeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
