using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace diplom.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestsPage : ContentPage
	{

        public static string correctVar = "";
        public TestsPage ()
		{
			InitializeComponent ();
            butt.Text = "Праверыць";
            if (ItemPageDinamic.wordRus.Count != 0)
            {
                var list1 = ItemPageDinamic.wordRus;
                var list2 = ItemPageDinamic.varCorr;
                var list3 = ItemPageDinamic.var1;
                var list4 = ItemPageDinamic.var2;
                var list5 = ItemPageDinamic.imgSour;
                correctVar = ItemPageDinamic.varCorr[0];
                Random rnd = new Random();
                int value = rnd.Next(0, 3);
                if (value == 1)
                {
                    rad1.Content = list2[0];
                    rad2.Content = list3[0];
                    rad3.Content = list4[0];
                    ItemPageDinamic.varCorr.RemoveAt(0);
                    ItemPageDinamic.var1.RemoveAt(0);
                    ItemPageDinamic.var2.RemoveAt(0);
                }
                else if (value == 2)
                {
                    rad2.Content = list2[0];
                    rad1.Content = list3[0];
                    rad3.Content = list4[0];
                    ItemPageDinamic.varCorr.RemoveAt(0);
                    ItemPageDinamic.var1.RemoveAt(0);
                    ItemPageDinamic.var2.RemoveAt(0);
                }
                else
                {
                    rad3.Content = list2[0];
                    rad1.Content = list3[0];
                    rad2.Content = list4[0];
                    ItemPageDinamic.varCorr.RemoveAt(0);
                    ItemPageDinamic.var1.RemoveAt(0);
                    ItemPageDinamic.var2.RemoveAt(0);
                }
                lbl.Text = list1[0];
                ItemPageDinamic.wordRus.RemoveAt(0);
                img.Source = list5[0];
                ItemPageDinamic.imgSour.RemoveAt(0);
            }
            else
            {
                Navigation.PushModalAsync(new ResultTestsPage());
            }

        }
        //public static List< string>list2;
        public void Method()
        {
            
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            //Method();
            try
            {
                if (butt.Text == "Далей")
                {
                    var currentPage = new TestsPage();

                    // Переход на ту же страницу
                    Application.Current.MainPage.Navigation.PushAsync(currentPage);

                }
                else
                {
                    bool isAnyRadioButtonChecked = rad1.IsChecked || rad2.IsChecked || rad3.IsChecked;

                    if (isAnyRadioButtonChecked)
                    {
                        // Получаем выбранный ответ
                        string selectedAnswer = "";
                        if (rad1.IsChecked)
                        {
                            if (rad1.Content == correctVar)
                            {
                                rad1.BorderColor = Color.Green;
                                rad1.BorderWidth = 2;
                                ItemPageDinamic.result += 1;
                            }
                            else
                            {
                                rad1.BorderColor = Color.Red;
                                rad1.BorderWidth = 2;
                            }
                            butt.Text = "Далей";
                        }
                        else if (rad2.IsChecked)
                        {
                            if (rad2.Content == correctVar)
                            {
                                rad2.BorderColor = Color.Green;
                                rad2.BorderWidth = 2;
                                ItemPageDinamic.result += 1;
                            }
                            else
                            {
                                rad2.BorderColor = Color.Red;
                                rad2.BorderWidth = 2;
                            }
                            butt.Text = "Далей";
                        }
                        else if (rad3.IsChecked)
                        {
                            if (rad3.Content == correctVar)
                            {
                                rad3.BorderColor = Color.Green;
                                rad3.BorderWidth = 2;
                                ItemPageDinamic.result += 1;
                            }
                            else
                            {
                                rad3.BorderColor = Color.Red;
                                rad3.BorderWidth = 2;
                            }
                            butt.Text = "Далей";
                            // Проверяем правильность выбранного ответа

                        }
                        else
                        {
                            // Ни один из RadioButton не выбран
                            await DisplayAlert("Ошибка", "Пожалуйста, выберите один из вариантов ответа.", "ОК");
                        }
                        
                    }
                }
            }
            catch(Exception ex)
            {
                Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);
            }
            
            
                
            
            
        }
    }
}