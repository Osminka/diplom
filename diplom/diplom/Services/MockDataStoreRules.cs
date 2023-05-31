using diplom.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diplom.Services
{
    internal class MockDataStoreRules : IDataStore2<Item2>
    {
        readonly List<Item2> items = new List<Item2>();

        public MockDataStoreRules()
        {
            try
            {
                var conn = new MySqlConnection(Properties.Resources.db_mobile);
                conn.Open();
                var command = new MySqlCommand(@"SELECT * FROM Rules", conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Чтение данных из текущей строки результата
                        string name = reader.GetString("Name");
                        string text = reader.GetString("Text");

                        // Обработка полученных данных
                        //Console.WriteLine($"topic: {topic}, word: {word}");
                        var item = new Item2 { Id = Guid.NewGuid().ToString(),  Name = name, Text = text };
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
        public static async void AddEntrie(string name, string text)
        {
            try
            {
                using (var conn = new MySqlConnection(Properties.Resources.db_mobile))
                {
                    conn.Open();
                    var command = new MySqlCommand("SELECT COUNT(*) FROM Rules WHERE Name = @name", conn);
                    command.Parameters.AddWithValue("@name", name);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Внимание!", "Такая тема уже есть", "Ok");
                    }
                    else
                    {
                        Ok = "ok";
                        command = new MySqlCommand("INSERT INTO Rules (Name, Text) VALUES (@name, @text)", conn);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@text", text);

                        command.ExecuteNonQuery();

                        await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Внимание!", "Запись добавлена", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Внимание!", "Произошла ошибка, проверьте ваше подключение к интернету", "Ok");
                Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);
                //await App.Current.MainPage.DisplayAlert("Внимание!", "Проверьте ваше подключение к интернету!", "Ok");
            }

        }
        public static async void UpdateEntrie(string oldName, string name, string text)
        {
            try
            {
                var conn = new MySqlConnection(Properties.Resources.db_mobile);
                conn.Open();

                var command = new MySqlCommand(@"UPDATE RUles SET Name = @name, Text = @text WHERE Name = @oldName", conn);
                command.Parameters.AddWithValue("@text", text);
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

                var command = new MySqlCommand(@"DELETE FROM Rules WHERE Name = @name", conn);
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
        public static string text;
        public async Task<bool> AddItemAsync(Item2 item)
        {
            AddEntrie(item.Name, item.Text);
            if (Ok == "ok")
            {
                items.Add(item);
                Ok = "";
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item2 item, string id)
        {
            var oldItem = items.Where((arg) => arg.Id == id).FirstOrDefault();
            UpdateEntrie(oldItem.Text, item.Text, item.Name);
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
            DeleteEntrie(oldItem.Text);

            if (Ok == "ok")
            {
                items.Remove(oldItem);
                Ok = "";
            }
            return await Task.FromResult(true);
        }

        public async Task<Item2> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item2>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
