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

        private UserService _userService;

        public UserViewModel()
        {
            _userService = new UserService();
            LoadUsersAsync();
        }


        // Método asíncrono para cargar usuarios
        private async Task LoadUsersAsync()
        {
            var userList = await _userService.GetUsersAsync(); // Obtiene los usuarios de la API
            Users = new ObservableCollection<User>(userList); // Actualiza la propiedad observable
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
