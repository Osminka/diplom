using App1.ViewModels;
using diplom;
using diplom.Views;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrPage : ContentPage
    {
        public RegistrPage()
        {
            InitializeComponent();
            this.BindingContext = new RegistrViewModel();
            ClickedLabel();

        }
        public async void Method()
        {
            await Navigation.PushAsync(new LoginPage());
        }
        public void ClickedLabel()
        {
            TapGestureRecognizer tapGesture = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 2
            };
            tapGesture.Tapped += (s, e) =>
            {
                Method();
            };
            loginPage.GestureRecognizers.Add(tapGesture);

        }
      
        public void CreateLocalDb()
        {
            Users users = new Users
            {
                Email = email.Text,
                trueORfalse = true,
            };

            App.Db.SaveItem(users);
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            string Username = username.Text;
            string Email = email.Text;
            string Pass = password.Text;
            if (Email == "" || Pass == "" || Username == "")
            {
                await App.Current.MainPage.DisplayAlert("Внимание!", "Заполните пустые поля!", "Ok");
            }
            else
            {
                try
                {
                    //MySqlDataReader count;
                    //using (var conn = new MySqlConnection(diplom.Properties.Resources.db_mobile))
                    //{
                    //    conn.Open();
                    //    MySqlCommand command = new MySqlCommand();
                    //    command.CommandText = @"SELECT * FROM Users WHERE username=N'" + Username + "'";
                    //    command.Connection = conn;
                    //    count = command.ExecuteReader();
                    //    conn.Close();
                    //}                    
                    var connect = new MySqlConnection(diplom.Properties.Resources.db_mobile);
                    connect.Open();
                    MySqlCommand comm = new MySqlCommand();
                    comm.CommandText = @"SELECT * FROM Users WHERE email=N'" + Email + "'";
                    comm.Connection = connect;
                    var count2 = comm.ExecuteReader();
                    //Console.WriteLine(count.HasRows);
                    if (count2.HasRows)
                    {
                        await App.Current.MainPage.DisplayAlert("Внимание!", "Пользователь с таким именем уже существует", "Ok");
                    }
                    //else if (count2.HasRows)
                    //{
                    //    await App.Current.MainPage.DisplayAlert("Внимание!", "Пользователь с таким емеилом уже существует", "Ok");

                    //}
                    else
                    {
                        connect.Close();
                        connect.Open();
                        comm = new MySqlCommand();
                        comm.CommandText = @"INSERT INTO Users (username,password, email) VALUES (N'" + Username + "',N'" + Pass + "',N'" + Email + "')";
                        comm.Connection = connect;
                        comm.ExecuteNonQuery();
                        //CreateLocalDb();
                        //await Shell.Current.GoToAsync(nameof(AppShell));
                        //await DisplayPromptAsync("Внимание!", "Пойдет!", "Ok");
                        await App.Current.MainPage.DisplayAlert("Внимание!", "Пойдет!", "Ok");
                        //await Navigation.PushAsync(new ItemsPage());
                        //await Navigation.PopModalAsync();
                        //await Navigation.PopModalAsync();
                        await Navigation.PushAsync(new AppShell());
                        //await Navigation.PopAsync();
                    }
                    connect.Close();

                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);
                    await App.Current.MainPage.DisplayAlert("Внимание!", "Проверьте ваше подключение к интернету!", "Ok");
                }

            }





            Users users = new Users
            {
                Email = email.Text.ToString(),
                trueORfalse = true,
            };

            App.Db.SaveItem(users);
        }
    }
}