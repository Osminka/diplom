using System;
using System.Linq;
using diplom.ViewModels;

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

        private async void toolbar1_Clicked(object sender, System.EventArgs e)
        {
            var mass = descrip.Text.Split('-');
            for (int i = 0; i < mass.Length; i++)
            {
                await Console.Out.WriteLineAsync(mass[i]);
            }
            
            await Navigation.PushModalAsync(new TestsPage());
        }
    }
}