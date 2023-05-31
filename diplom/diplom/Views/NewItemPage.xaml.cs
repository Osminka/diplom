using diplom.Models;
using diplom.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace diplom.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            

        }

        //private void entry2_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (entry1.Text != "" || entry2.Text != "") { butt.BackgroundColor = Color.Black; butt.TextColor = Color.White; }
        //}


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