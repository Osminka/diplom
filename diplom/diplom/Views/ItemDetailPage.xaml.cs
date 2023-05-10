using diplom.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace diplom.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}