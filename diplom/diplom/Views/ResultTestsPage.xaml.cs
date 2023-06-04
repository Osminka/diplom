using App1.Views;
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
	public partial class ResultTestsPage : ContentPage
	{
		public ResultTestsPage ()
		{
			InitializeComponent ();
			if(ItemPageDinamic.result<4) reslbl.TextColor = Color.Red;

            reslbl.Text = ItemPageDinamic.result + "/6";
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            
            Application.Current.MainPage = new AppShell();
            
        }
    }
}