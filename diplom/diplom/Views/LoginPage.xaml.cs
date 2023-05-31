using App1.Views;
using diplom.ViewModels;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace diplom.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
            //SearchInLocalDb();
            ClickedLabel();
            ForgotPass();
            //SearchInLocal
            //
            //
            //
            //
            //();

        }
        public async void Method()
        {
            await Navigation.PushModalAsync(new RegistrPage());
        }
        public void ClickedLabel()
        {
            TapGestureRecognizer tapGesture = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1
            };
            tapGesture.Tapped += (s, e) =>
            {
                Method();
            };
            registrPage.GestureRecognizers.Add(tapGesture);

        }

        //public async void Method2()
        //{
        //    //await Navigation.PushAsync(new RegistrPage());
        //}
        public void ForgotPass()
        {
            TapGestureRecognizer tapGesture = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1
            };
            tapGesture.Tapped += (s, e) =>
            {
                //Method2();
            };
            registrPage.GestureRecognizers.Add(tapGesture);

        }
        public void CreateLocalDb()
        {
            Users users = new Users
            {
                Email = emailEntry.Text,
                trueORfalse = true,
            };

            App.Db.SaveItem(users);
        }

        public async void SearchInLocalDb()
        {
            var mass = App.Db.Search();
            if(mass.Count!=0)
            {
                await Navigation.PopModalAsync();
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            string Email = emailEntry.Text;
            string Pass = passEntry.Text;

            if (Email == "" || Pass == "")
            {
                await App.Current.MainPage.DisplayAlert("Внимание!", "Заполните пустые поля!", "Ok");
            }
            else
            {
                try
                {
                    var conn = new MySqlConnection(Properties.Resources.db_mobile);
                    conn.Open();
                    var command = new MySqlCommand(@"SELECT * FROM Users WHERE email=N'" + Email + "' AND password=N'" + Pass + "'", conn);

                    var count = command.ExecuteReader();
                    if (count.Read())
                    {
                        CreateLocalDb();
                        await App.Current.MainPage.DisplayAlert("Внимание!", "Пойдет", "Ok");
                        //await Navigation.PopModalAsync();
                        Application.Current.MainPage = new AppShell();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Внимание!", "Неправильный email или пароль", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);
                    await App.Current.MainPage.DisplayAlert("Внимание!", "Проверьте ваше подключение к интернету!", "Ok");
                }

            }

        }
    
    }
}