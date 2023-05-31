using diplom.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace diplom.Views
{
    public partial class LitDetailPage : ContentPage
    {
        public LitDetailPage()
        {
            InitializeComponent();
            BindingContext = new LitDetailViewModel();
        }
        
    }
}