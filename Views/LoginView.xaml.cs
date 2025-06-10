using System.Windows;
using System.Windows.Controls;

namespace BiblioGest.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            PasswordBox.PasswordChanged += (s, e) =>
            {
                if (DataContext is ViewModels.LoginViewModel vm)
                {
                    vm.MotDePasse = PasswordBox.Password;
                }
            };
        }
    }
}