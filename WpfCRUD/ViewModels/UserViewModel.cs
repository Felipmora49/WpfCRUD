using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfCRUD.BD;
using WpfCRUD.Commands;
using WpfCRUD.Models;
using WpfCRUD.Views;

namespace WpfCRUD.ViewModels
{
    internal class UserViewModel : ViewModelBase
    {
        private readonly Bd bd;
        private ObservableCollection<UserModel> _users;
        private UserModel _selectedUser;

        public UserViewModel()
        {
            bd = new Bd();
            _users = bd.Get();
        }

        public ObservableCollection<UserModel> Users
        {
            get => _users;
            set
            {
                if (_users != value)
                {
                    _users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }
        }

        public UserModel SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    // Asignar el usuario seleccionado a la propiedad User para que se muestren sus datos en el formulario
                    User = _selectedUser;
                    OnPropertyChanged(nameof(SelectedUser));
                }
            }
        }

        private UserModel _user;

        public UserModel User
        {
            get => _user;
            set
            {
                if (_user != value)
                {
                    _user = value;
                    OnPropertyChanged(nameof(User));
                }
            }
        }

        public ICommand AddCommand => new RelayCommand(AddExecute, AddCanExecute);

        private void AddExecute(object user)
        {
            bd.Add(User);
            Users = bd.Get();
        }

        private bool AddCanExecute(object user)
        {
            return true;
        }

        public ICommand EditCommand => new RelayCommand(EditExecute, EditCanExecute);

        private void EditExecute(object user)
        {
            bd.Edit(User);
            Users = bd.Get();
        }

        private bool EditCanExecute(object user)
        {
            return User != null;
        }

        public ICommand DeleteCommand => new RelayCommand(DeleteExecute, DeleteCanExecute);

        private void DeleteExecute(object user)
        {
            if (SelectedUser != null)
            {
                bd.Delete(SelectedUser);
                Users = bd.Get();
            }
        }

        private bool DeleteCanExecute(object user)
        {
            return SelectedUser != null;
        }

        public ICommand GoToRecordViewCommand => new RelayCommand(GoToRecordViewExecute);

        private void GoToRecordViewExecute(object parameter)
        {
            // Crear una instancia de la nueva vista
            var loginView = new LoginView();

            // Obtener la ventana principal actual
            var mainWindow = App.Current.MainWindow;

            // Cambiar la propiedad Content de la ventana principal a la nueva vista
            mainWindow.Content = loginView;
        }
    }
}
