using diplom.ViewModels;

using Xamarin.Forms;

namespace diplom.Views
{
    public partial class LitPageDinamic : ContentPage
    {
        public LitPageDinamic()
        {
            InitializeComponent();
            BindingContext = new DinamicDetailViewModelLit();
            //ToolbarItems.Add(new ToolbarItem("Назад", null, async () =>
            //{
            //    await Shell.Current.GoToAsync(".."); // Переход назад на предыдущую страницу
            //}));
            
        }

        //private async void toolbar1_Clicked(object sender, System.EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new TestsPage());
        //}
    }
}