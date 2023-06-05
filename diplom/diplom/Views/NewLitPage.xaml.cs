using diplom.Models;
using diplom.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace diplom.Views
{
    public partial class NewLitPage : ContentPage
    {
        public ItemLit Item { get; set; }

        public NewLitPage()
        {
            InitializeComponent();
            BindingContext = new NewLitViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            

        }

        

        private void butt_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (butt6.IsEnabled == false)
            {
                butt6.BackgroundColor = Color.LightGray;
                butt6.TextColor = Color.White;
            }
            if (butt6.IsEnabled == true)
            {
                butt6.BackgroundColor = Color.FromHex("#bf3f3d");
                butt6.TextColor = Color.White;
            }
        }

        
        //private void Editor_PropertyChanging(object sender, Xamarin.Forms.PropertyChangingEventArgs e)
        //{
        //    if (entry1.Text!="" || entry2.Text != "") { butt.BackgroundColor = Color.LightGray; butt.TextColor = Color.Black; }
        //}
        //public void MethodButt()
        //{
        //    butt.TextColor  = Color.White;
        //}
    }
}