using diplom.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace diplom.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            Title = "Login";
            //LoginCommand = new Command(OnLoginClicked);
        }
        // public static string Email = "";
        //public static string Pass = "";


    }
}
