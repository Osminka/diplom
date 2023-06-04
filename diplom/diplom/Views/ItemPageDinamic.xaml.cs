using System;
using System.Collections.Generic;
using System.Linq;
using diplom.ViewModels;
using MySqlConnector;
using Xamarin.Forms;

namespace diplom.Views
{
    public partial class ItemPageDinamic : ContentPage
    {
        public ItemPageDinamic()
        {
            InitializeComponent();
            BindingContext = new DinamicDetailViewModel();
            //ToolbarItems.Add(new ToolbarItem("Назад", null, async () =>
            //{
            //    await Shell.Current.GoToAsync(".."); // Переход назад на предыдущую страницу
            //}));
            
        }
        public static List<string> mass = new List<string>();
        //
        public static List<string> wordRus = new List<string>();
        public static List<string> varCorr = new List<string>();
        public static List<string> var1 = new List<string>();
        public static List<string> var2 = new List<string>();
        public static List<string> imgSour = new List<string>();
        public static int result=0;
        private async void toolbar1_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var conn = new MySqlConnection(Properties.Resources.db_mobile);
                conn.Open();
                var command = new MySqlCommand(@"SELECT * FROM Words WHERE topic=N'" + descrip.Text + "'", conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Чтение данных из текущей строки результата

                        string wordrus = reader.GetString("WordRus");
                        string varcorr = reader.GetString("VariantCorrect");
                        string variant1 = reader.GetString("Variant1");
                        string variant2 = reader.GetString("Variant2");
                        string imgsour = reader.GetString("ImageSource");
                        if (wordrus != "")
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                //var list = ItemPageDinamic.mass.ToArray();
                                var str = wordrus.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                                wordRus.Add(str[i].ToString());
                                str = varcorr.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                                varCorr.Add(str[i].ToString());
                                str = variant1.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                                var1.Add(str[i].ToString());
                                str = variant2.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                                var2.Add(str[i].ToString());
                                str = imgsour.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                                imgSour.Add(str[i].ToString());

                            }
                            
                            await Navigation.PushModalAsync(new TestsPage());
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Увага!", "На дадзены момант тэст у распрацоўцы", "Ok");
                        }
                        // Обработка полученных данных
                        //Console.WriteLine($"topic: {topic}, word: {word}");
                        //var item = new Item { Id = Guid.NewGuid().ToString(), Text = topic, Description = word };
                        //items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Увага!", "Праверце ваша падлучэнне", "Ok");
                Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);

            }


            //var mass = descrip.Text.Split('-');
            //for (int i = 0; i < mass.Length; i++)
            //{
            //    await Console.Out.WriteLineAsync(mass[i]);
            //}


        }
    }
}