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
        }
       
    }
}