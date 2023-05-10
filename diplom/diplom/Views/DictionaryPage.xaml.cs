using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HtmlAgilityPack;
using Java.Sql;
using System.Net;
using System.Text.RegularExpressions;
using System.Linq;


namespace diplom.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class DictionaryPage : ContentPage
    {
       

        public DictionaryPage()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            InitializeComponent();

        }
        static readonly HttpClient client = new HttpClient();
        static string checkboxRes = "belrus";
        private async void Button_Clicked(object sender, EventArgs e)
        {
            resultLabel.Text = "";
            resultLabel2.Text = "";
            textLabel.Text = "";
            
            HttpResponseMessage response = await client.GetAsync($"https://www.skarnik.by/search_json?term={WordEntry.Text}&lang=rus");
            // получаем HTML-код страницы
            //var config = Configuration.Default.WithDefaultLoader();

            if (checkboxRes == "rusbel")
            {
                resultLabel.Text = "Перевод:";
                resultLabel.FontAttributes = FontAttributes.Italic;
                resultLabel.TextColor = Color.Black;
                var context = new HtmlAgilityPack.HtmlDocument();
                string responseBody = await response.Content.ReadAsStringAsync();
                string firstdata = responseBody;
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(firstdata);
                int Id = data[0].id;
                var html = await GetHtmlAsync($"https://www.skarnik.by/rusbel/{Id}");

                context.LoadHtml(html);

                // получаем элемент <p id="trn">
                var trnElement = context.GetElementbyId("trn");
                var word = trnElement.Descendants("font").FirstOrDefault(n => n.GetAttributeValue("color", "").Equals("831b03"));

                string resultText = word.InnerText.Replace("&nbsp;", "");
                textLabel.IsVisible = true;
                textLabel.Text = resultText;
                resultLabel2.Text = trnElement.InnerText.Replace("&nbsp;", "").Trim();
            }
            else if (checkboxRes == "belrus")
            {
                resultLabel.Text = "Перавод:";
                resultLabel.FontAttributes = FontAttributes.Italic;
                resultLabel.TextColor = Color.Black;
                string word = WordEntry.Text; // замените на нужное слово
                string url = $"https://www.skarnik.by/search?term={word}&lang=bel";

                WebClient client = new WebClient();
                string html = client.DownloadString(url);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                var nrtElement = doc.DocumentNode.SelectSingleNode("//span[@class='nrt']");
                var trnElement = doc.GetElementbyId("trn");
                //var NewTrnElement = trnElement.InnerHtml.Replace("<br>", "\n");
                if (trnElement != null)
                {
                    ////var text = nrtElement.InnerText;
                    // делайте что-то с полученным текстом...
                    textLabel.IsVisible = true;
                    textLabel.Text = WordEntry.Text;
                    resultLabel2.Text = trnElement.InnerText.Replace("&nbsp;", "").Trim();
                }
            }
            else
            {
                resultLabel.Text = "Тлумачэнне слова:";
                resultLabel.FontAttributes = FontAttributes.Italic;
                resultLabel.TextColor = Color.Black;
                string word = WordEntry.Text; // замените на нужное слово
                string url = $"https://www.skarnik.by/search?term={word}&lang=beld";

                WebClient client = new WebClient();
                string html = client.DownloadString(url);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                //var nrtElement = doc.DocumentNode.SelectSingleNode("//[@class='nrt']");
                var trnElement = doc.GetElementbyId("trn");
                if (trnElement != null)
                {
                    ////var text = nrtElement.InnerText;
                    // делайте что-то с полученным текстом...
                    textLabel.IsVisible = true;
                    textLabel.Text = WordEntry.Text;
                    resultLabel2.Text = trnElement.InnerText.Replace("&nbsp;", "").Trim();
                }
            }

        }

        static async Task<string> GetHtmlAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        private void CheckBox_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
        {
            checkboxRes = "rusbel";
            textLabel.Text = "";
            resultLabel.Text = "Русско - белорусский словарь";
            resultLabel2.Text = "За основу взят академический словарь, который был выпущен в 1953 году (под редакцией Я. Коласа, К. Крапивы и П. Глебки) и затем несколько раз переиздавался с исправлениями и дополнениями.";
        }

        private void CheckBox_CheckedChanged_2(object sender, CheckedChangedEventArgs e)
        {
            checkboxRes = "belrus";
            textLabel.Text = "";
            resultLabel.Text = "Беларуска-рускі слоўнік";
            resultLabel2.Text = "Раздзел зроблены з акадэмічнага руска-беларускага слоўніка-трохтомніка. Складзены «аўтаматычна адваротным чынам», таму ёсць пэўныя недахопы, якія паступова выпраўляюцца. Атрымаўся, так бы мовіць, «слоўнік наадварот». Дарэчы, у некаторых артыкулах вы ўбачыце беларускія сінонімы. Карыстайцеся, хваліце, крытыкуйце.";
        }

        private void CheckBox_CheckedChanged_3(object sender, CheckedChangedEventArgs e)
        {
            checkboxRes = "beld";
            textLabel.Text = "";
            resultLabel.Text = "Тлумачальны слоўнік беларускай мовы";
            resultLabel2.Text = "Тлумачальны слоўнік на аснове пяцітомніка 1977-1984 гг. (акадэмічнае выданне пад рэдакцыяй К. Крапівы). Адаптаваны згодна з сучаснымі рэаліямі.";

        }


    }
    public static class StringExtensions
    {
        public static string UpperFirstChar(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}