using DadJokeBotLibrary;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace DadJokeBot
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Private Members
        private IHost? host;
        #endregion

        #region Overrides
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            host = Host.CreateDefaultBuilder()
           .ConfigureServices((hostContext, services) =>
           {
               services.AddScoped<DadJokeViewModel>();
               services.AddScoped<IJokeClient, JokeClient>();
               services.AddScoped<IJokeGenerator, JokeGenerator>();              
               services.AddSingleton<DadJokeWindow>();
           }).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var personWindow = services.GetRequiredService<DadJokeWindow>();
                    personWindow.Show();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Occured" + ex.Message);
                }
            }
        }
        #endregion
    }
}
