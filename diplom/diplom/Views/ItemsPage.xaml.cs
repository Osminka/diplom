using diplom.Models;
using diplom.ViewModels;
using diplom.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace diplom.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        bool adminOrno = App.AdmOrNo;
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
            if(adminOrno == true)
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