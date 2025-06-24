using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using FarmaControl_App.Models;


namespace FarmaControl_App.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<User> users;

        public UserViewModel()
        {
            Users = new ObservableCollection<User>
            {
                new User { Name = "Joaco", Username = "UsuarioJoaco", Role = "Admin", Email = "joaco@example.com" },
            new User { Name = "Simon", Username = "UsuarioSimon", Role = "Editor", Email = "simon@example.com" },
            new User { Name = "Pepe", Username = "UsuarioPepe", Role = "Viewer", Email = "pepe@example.com" }
            };
        }

    }
}
