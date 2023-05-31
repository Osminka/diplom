using diplom.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace diplom.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RulesPage : ContentPage
    {
        RulesViewModel _viewModel;
        bool adminOrno = App.AdmOrNo;
        public RulesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new RulesViewModel();
            if (adminOrno == true)
            {
                toolbar1.Text = "Дадаць";
            }
            else
            {
                toolbar1.Text = null;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}