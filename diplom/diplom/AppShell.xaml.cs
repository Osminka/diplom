using diplom.ViewModels;
using diplom.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Resources;
namespace diplom
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemPageDinamic), typeof(ItemPageDinamic));
            Routing.RegisterRoute(nameof(webviewPage), typeof(webviewPage));
            Routing.RegisterRoute(nameof(LitDetailPage), typeof(LitDetailPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(NewTestPage), typeof(NewTestPage));
            Routing.RegisterRoute(nameof(NewLitPage), typeof(NewLitPage));
            Routing.RegisterRoute(nameof(NewItemRulePage), typeof(NewItemRulePage));
            Routing.RegisterRoute(nameof(ItemDetailRulePage), typeof(ItemDetailRulePage));
            Routing.RegisterRoute(nameof(LitPageDinamic), typeof(LitPageDinamic));
            Routing.RegisterRoute(nameof(RuleItemPageDinamic), typeof(RuleItemPageDinamic));
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
