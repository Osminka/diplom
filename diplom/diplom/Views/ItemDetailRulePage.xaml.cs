using diplom.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace diplom.Views
{
    public partial class ItemDetailRulePage : ContentPage
    {
        public ItemDetailRulePage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailRuleViewModel();
        }
    }
}