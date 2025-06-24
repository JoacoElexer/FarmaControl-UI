using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using FarmaControl_App.Models;
using FarmaControl_App.Services;


namespace FarmaControl_App.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<User> users;

        private ObservableCollection<User> _allUsers;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Users))] // Notifica a 'Users' cuando 'SearchText' cambia
        private string searchText;

        private UserService _userService;

        public UserViewModel()
        {
            _userService = new UserService();
            LoadUsersAsync();

            // Inicializa _allUsers para evitar NullReferenceException al inicio si SearchText se establece antes.
            _allUsers = new ObservableCollection<User>();
            Users = new ObservableCollection<User>();
        }

        partial void OnSearchTextChanged(string value)
        {
            FilterUsers(value);
        }

        private async Task LoadUsersAsync()
        {
            var userList = await _userService.GetUsersAsync(); // Obtiene los usuarios de la API
            Users = new ObservableCollection<User>(userList); // Actualiza la propiedad observable
        }

        private void FilterUsers(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                Users = new ObservableCollection<User>(_allUsers);
            }
            else
            {
                var UsuariosFiltrados = _allUsers.Where(u =>
                u.Username.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();
                Users = new ObservableCollection<User>(UsuariosFiltrados);
            }
        }

        // Aquí podrías añadir comandos para añadir, editar o eliminar usuarios
        // Por ejemplo:
        // [RelayCommand]
        // public async Task AddNewUser(User newUser) {
        //     await _userService.SaveUserAsync(newUser);
        //     await LoadUsersAsync(); // Recarga la lista después de añadir
        // }

    }
}