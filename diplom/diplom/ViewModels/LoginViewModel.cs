using diplom.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace diplom.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command CancelCommand { get; }
        public Command LoginCommand { get; }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        public LoginViewModel()
        {
            Title = "Login";
            CancelCommand = new Command(OnCancel);
            //LoginCommand = new Command(OnLoginClicked);
        }
        // public static string Email = "";
        //public static string Pass = "";


    }
}
