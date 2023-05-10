using diplom.ViewModels;
using diplom.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace diplom
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemPageDinamic), typeof(ItemPageDinamic));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            //Method();
            
        }

        public async void Method()
        {
            var loginPage = new LoginPage();
            await Navigation.PushModalAsync(loginPage);
            //await Shell.Current.GoToAsync("//LoginPage");

        }

    }
}
