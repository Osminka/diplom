using diplom.ViewModels;

using Xamarin.Forms;

namespace diplom.Views
{
    public partial class RuleItemPageDinamic : ContentPage
    {
        public RuleItemPageDinamic()
        {
            InitializeComponent();
            BindingContext = new DinamicDetailRuleViewModel();
            //ToolbarItems.Add(new ToolbarItem("Назад", null, async () =>
            //{
            //    await Shell.Current.GoToAsync(".."); // Переход назад на предыдущую страницу
            //}));
            
        }
       
    }
}