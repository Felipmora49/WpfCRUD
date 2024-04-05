using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfCRUD.BD;
using WpfCRUD.Commands;
using WpfCRUD.Models;
using WpfCRUD.Views;

namespace WpfCRUD.ViewModels
{
    internal class LoginViewModel : ViewModelBase
    {
        private string _email;
        private string _password;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand => new RelayCommand(LoginExecute);

        private void LoginExecute(object parameter)
        {
            // Verificar que el usuario y la contraseña no estén vacíos
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Por favor, ingrese su correo y contraseña.");
                return;
            }

            // Llamar al método Login de tu clase Bd para verificar las credenciales
            Bd bd = new Bd();
            UserModel user = new UserModel { Email = Email, Password = Password };
            UserModel loggedInUser = bd.Login(user);

            // Validar el resultado del inicio de sesión
            if (loggedInUser != null && loggedInUser.Email == Email && loggedInUser.Password == Password)
            {
                // Credenciales válidas, navegar a la vista RecordView.
                var recordView = new RecordView();
                var mainWindow = App.Current.MainWindow;
                mainWindow.Content = recordView;
            }
            else
            {
                // Credenciales inválidas, mostrar un mensaje de error.
                MessageBox.Show("Correo o contraseña incorrectos. Por favor, inténtelo de nuevo.");
            }
        }


    }
}
