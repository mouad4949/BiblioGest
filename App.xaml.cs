using BiblioGest.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace BiblioGest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    services.AddSingleton<MainViewModel>(provider => new MainViewModel(
    provider.GetService<Data.BiblioGestContext>(),
    provider.GetService<LivreViewModel>(),
    provider.GetService<AdherentViewModel>(),
    provider.GetService<EmpruntViewModel>()));
    }

}
