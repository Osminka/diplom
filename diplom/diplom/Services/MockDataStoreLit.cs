using diplom.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static Android.Graphics.ImageDecoder;

namespace diplom.Services
{
    internal class MockDataStoreLit : IDataStoreLit<ItemLit>
    {
        readonly List<ItemLit> items = new List<ItemLit>();

        public MockDataStoreLit()
        {
            try
            {
                var conn = new MySqlConnection(Properties.Resources.db_mobile);
                conn.Open();
                var command = new MySqlCommand(@"SELECT * FROM Literature", conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Чтение данных из текущей строки результата
                        string section = reader.GetString("Source");
                        string section2 = reader.GetString("ImageSource");
                        string name = reader.GetString("Name");
      

                        // Обработка полученных данных
                        //Console.WriteLine($"topic: {topic}, word: {word}");
                        var item = new ItemLit { Id = Guid.NewGuid().ToString(), Source = section, ImageSource = section2, Name = name };
                        items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Внимание!", "Произошла ошибка, проверьте ваше подключение к интернету", "Ok");
                Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);
            }
        }

        static MySqlDataReader Count;
        static string Ok = "";
        public static async void AddEntrie(string name, string source, string imageSource)
        {
            try
            {
                var conn = new MySqlConnection(Properties.Resources.db_mobile);
                conn.Open();
                var command = new MySqlCommand(@"SELECT * FROM Literature WHERE Name=N'" + name + "'", conn);

                var count = command.ExecuteReader();
                Count = count;
                if (count.HasRows)
                {
                    Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Внимание!", "Такая тема уже есть", "Ok");
                }
                else
                {
                    count.Close();
                    Ok = "ok";
                    command = new MySqlCommand(@"INSERT INTO Literature (Source, Name, ImageSource) VALUES (N'" + source + "', N'" + name + "', N'" + imageSource + "')", conn);
                    command.ExecuteNonQuery();

                    conn.Close();
                    Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Внимание!", "Запись добавлена", "Ok");
                }


            }
            catch (Exception ex)
            {
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Внимание!", "Произошла ошибка, проверьте ваше подключение к интернету", "Ok");
                Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);
                //await App.Current.MainPage.DisplayAlert("Внимание!", "Проверьте ваше подключение к интернету!", "Ok");
            }

        }
        public static async void UpdateEntrie(string oldName, string name, string source, string imageSource)
        {
            try
            {
                var conn = new MySqlConnection(Properties.Resources.db_mobile);
                conn.Open();

                var command = new MySqlCommand(@"UPDATE Literature SET Name = @name, Source = @source,ImageSource = @imageSource, WHERE Name = @oldName", conn);
                command.Parameters.AddWithValue("@source", source);
                command.Parameters.AddWithValue("@imageSource", imageSource);
                command.Parameters.AddWithValue("@newName", name);
                command.Parameters.AddWithValue("@oldName", oldName);
                command.ExecuteNonQuery();

                conn.Close();
                Ok = "ok";
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Внимание!", "Запись обновлена", "Ok");


            }
            catch (Exception ex)
            {
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Внимание!", "Произошла ошибка, проверьте ваше подключение к интернету", "Ok");
                Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);
                //await App.Current.MainPage.DisplayAlert("Внимание!", "Проверьте ваше подключение к интернету!", "Ok");
            }

        }
        public static async void DeleteEntrie(string name)
        {
            try
            {
                var conn = new MySqlConnection(Properties.Resources.db_mobile);
                conn.Open();

                var command = new MySqlCommand(@"DELETE FROM Literature WHERE Name = @name", conn);
                command.Parameters.AddWithValue("@name", name);
                command.ExecuteNonQuery();

                conn.Close();
                Ok = "ok";
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Внимание!", "Запись удалена", "Ok");
            }
            catch (Exception ex)
            {
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Внимание!", "Произошла ошибка, проверьте ваше подключение к интернету", "Ok");
                Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);
                //await App.Current.MainPage.DisplayAlert("Внимание!", "Проверьте ваше подключение к интернету!", "Ok");
            }

        }
        public static async void PdfDown(ItemLit item)
        {
            await Console.Out.WriteLineAsync("dff");
            try
            {
                var url = item.Source; // URL-адрес файла PDF
                await Launcher.OpenAsync(new Uri(url));
            }
            catch (Exception ex)
            {
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Внимание!", "Произошла ошибка, проверьте ваше подключение к интернету", "Ok");
                Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);
                //await App.Current.MainPage.DisplayAlert("Внимание!", "Проверьте ваше подключение к интернету!", "Ok");
            }

        }
        public static string text;
        public async Task<bool> AddItemAsync(ItemLit item)
        {
            AddEntrie(item.Source, item.Name, item.ImageSource);
            if (Ok == "ok")
            {
                items.Add(item);
                Ok = "";
            }

            return await Task.FromResult(true);
        }
        public async Task<bool> DownloadPdf(ItemLit item)
        {
            await Console.Out.WriteLineAsync(   "dff");
            PdfDown(item);
            //items.(item);
            //if (Ok == "ok")
            //{
            //    items.Add(item);
            //    Ok = "";
            //}
            //AddEntrie(item.Name, item.Source, item.ImageSource);
            //if (Ok == "ok")
            //{
            //    items.Add(item);
            //    Ok = "";
            //}

            return await Task.FromResult(true);
           
        }

        public async Task<bool> UpdateItemAsync(ItemLit item, string id)
        {
            var oldItem = items.Where((arg) => arg.Id == id).FirstOrDefault();
            UpdateEntrie(oldItem.Name, item.Name, item.Source, item.ImageSource);
            if (Ok == "ok")
            {
                items.Remove(oldItem);
                items.Add(item);
                Ok = "";
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((arg) => arg.Id == id).FirstOrDefault();
            DeleteEntrie(oldItem.Name);

            if (Ok == "ok")
            {
                items.Remove(oldItem);
                Ok = "";
            }
            return await Task.FromResult(true);
        }

        public async Task<ItemLit> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ItemLit>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
