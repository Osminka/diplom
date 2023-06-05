using diplom.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Android.Content.Res;
using System.Diagnostics;
using System.Linq.Expressions;
using MySqlConnector;
using Xamarin.Forms;
using static Android.Provider.UserDictionary;

namespace diplom.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items = new List<Item>();

        public MockDataStore()
        {
            try
            {
                var conn = new MySqlConnection(Properties.Resources.db_mobile);
                conn.Open();
                var command = new MySqlCommand(@"SELECT * FROM Words", conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Чтение данных из текущей строки результата
                        string topic = reader.GetString("Topic");
                        string word = reader.GetString("Text");

                        string wordRus = reader.GetString("WordRus");
                        string varCorr = reader.GetString("VariantCorrect");
                        string var1 = reader.GetString("Variant1");
                        string var2 = reader.GetString("Variant2");
                        string imageSour = reader.GetString("ImageSource");

                        // Обработка полученных данных
                        //Console.WriteLine($"topic: {topic}, word: {word}");               
                        var item = new Item { Id = Guid.NewGuid().ToString(), Text = topic, Description = word, WordRus = wordRus, VariantCorrect = varCorr, Variant1 = var1, Variant2 = var2, ImageSource = imageSour };
                        items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Увага!", "Адбылася памылка, праверце ваша падлучэнне да інтэрнэту", "Ok");
                Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);
            }
        }

        static MySqlDataReader Count;
        static string Ok = "";
        public static async void AddEntrie(string topic, string text, string wordRus, string varCorr, string var1, string var2, string imageSource)
        {
            try
            {
                var conn = new MySqlConnection(Properties.Resources.db_mobile);
                conn.Open();              
                var command = new MySqlCommand(@"SELECT * FROM Words WHERE Topic=N'" + topic +"'", conn);

                var count = command.ExecuteReader();
                Count = count;
                if (count.HasRows)
                {
                    App.Current.MainPage.DisplayAlert("Увага!", "Такая тэма ўжо ёсць", "Ok");
                }
                else
                {
                    count.Close();
                    Ok = "ok";
                    string query = "INSERT INTO Words (Topic, Text, WordRus, VariantCorrect, Variant1, Variant2, ImageSource) VALUES (@Topic, @Text, @WordRus, @VariantCorrect, @Variant1, @Variant2, @ImageSource)";

                    using (conn = new MySqlConnection(Properties.Resources.db_mobile))
                    {
                        conn.Open();

                        using (command = new MySqlCommand(query, conn))
                        {
                            command.Parameters.AddWithValue("@Topic", topic);
                            command.Parameters.AddWithValue("@Text", text);
                            command.Parameters.AddWithValue("@WordRus", wordRus);
                            command.Parameters.AddWithValue("@VariantCorrect", varCorr);
                            command.Parameters.AddWithValue("@Variant1", var1);
                            command.Parameters.AddWithValue("@Variant2", var2);
                            command.Parameters.AddWithValue("@ImageSource", imageSource);

                            command.ExecuteNonQuery();
                        }
                    }


                    //command = new MySqlCommand(@"INSERT INTO Words (Topic, Text) VALUES (N'" + topic + "', N'" + text + "')", conn);
                    //command.ExecuteNonQuery();

                    //conn.Close();
                    App.Current.MainPage.DisplayAlert("Увага!", "Запіс дабаўлена", "Ok");
                }

            
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Увага!", "Адбылася памылка, праверце ваша падлучэнне да інтэрнэту", "Ok");
                Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);
                //await App.Current.MainPage.DisplayAlert("Внимание!", "Проверьте ваше подключение к интернету!", "Ok");
            }

        }
        public static async void UpdateEntrie(string oldTopic, string topic, string text, string wordRus, string varCorr, string var1, string var2, string imageSource)
        {
            try
            {
                var conn = new MySqlConnection(Properties.Resources.db_mobile);
                conn.Open();

                var command = new MySqlCommand(@"UPDATE Words SET Text = @text, Topic = @newTopic, WordRus = @wordRus, VariantCorrect = @varCorr, Variant1 = @var1, Variant2 = @var2, ImageSource = @imageSource WHERE Topic = @oldTopic", conn);
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@newTopic", topic);
                command.Parameters.AddWithValue("@WordRus", wordRus);
                command.Parameters.AddWithValue("@VarCorr", varCorr);
                command.Parameters.AddWithValue("@Var1", var1);
                command.Parameters.AddWithValue("@Var2", var2);
                command.Parameters.AddWithValue("@ImageSource", imageSource);
                command.Parameters.AddWithValue("@oldTopic", oldTopic);
                command.ExecuteNonQuery();

                conn.Close();
                Ok = "ok";
                App.Current.MainPage.DisplayAlert("Увага!", "Запіс абноўленa", "Ok");


            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Увага!", "Адбылася памылка, праверце ваша падлучэнне да інтэрнэту", "Ok");
                Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);
                //await App.Current.MainPage.DisplayAlert("Внимание!", "Проверьте ваше подключение к интернету!", "Ok");
            }

        }
        public static async void DeleteEntrie(string topic)
        {
            try
            {
                var conn = new MySqlConnection(Properties.Resources.db_mobile);
                conn.Open();

                var command = new MySqlCommand(@"DELETE FROM Words WHERE Topic = @topic", conn);
                command.Parameters.AddWithValue("@topic", topic);
                command.ExecuteNonQuery();

                conn.Close();
                Ok = "ok";
                App.Current.MainPage.DisplayAlert("Увага!", "Запіс выдаленa", "Ok");
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Увага!", "Адбылася памылка, праверце ваша падлучэнне да інтэрнэту", "Ok");
                Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);
                //await App.Current.MainPage.DisplayAlert("Внимание!", "Проверьте ваше подключение к интернету!", "Ok");
            }

        }
        public static string text;
        public async Task<bool> AddItemAsync(Item item)
        {
            AddEntrie(item.Text, item.Description, item.WordRus, item.VariantCorrect, item.Variant1, item.Variant2, item.ImageSource);
            if (Ok == "ok")
            {
                items.Add(item);
                Ok = "";
            }
                
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item, string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            UpdateEntrie(oldItem.Text, item.Text, item.Description, item.WordRus, item.VariantCorrect, item.Variant1, item.Variant2, item.ImageSource);
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
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            DeleteEntrie(oldItem.Text);
           
            if (Ok == "ok")
            {
                items.Remove(oldItem);
                Ok = "";
            }
            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}