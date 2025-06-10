using BiblioGest.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;

namespace BiblioGest.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private string _identifiant;
        private string _motDePasse;

        public string Identifiant
        {
            get => _identifiant;
            set => SetProperty(ref _identifiant, value);
        }

        public string MotDePasse
        {
            get => _motDePasse;
            set => SetProperty(ref _motDePasse, value);
        }

        public ICommand ConnexionCommand { get; }

        private readonly IServiceProvider _serviceProvider;

        public LoginViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            ConnexionCommand = new RelayCommand(Connexion, CanConnexion);
        }

        private bool CanConnexion(object parameter) => !string.IsNullOrEmpty(Identifiant) && !string.IsNullOrEmpty(MotDePasse);

        private void Connexion(object parameter)
        {
            // Simulation d'une authentification simple
            if (Identifiant == "admin" && MotDePasse == "password")
            {
                var mainView = new Views.MainView
                {
                    DataContext = _serviceProvider.GetService<MainViewModel>()
                };
                mainView.Show();
                (parameter as Window)?.Close();
            }
            else
            {
                MessageBox.Show("Identifiant ou mot de passe incorrect.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}