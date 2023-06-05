using diplom.Models;
using diplom.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace diplom.Views
{
    public partial class NewItemRulePage : ContentPage
    {
        public Item2 Item { get; set; }

        public NewItemRulePage()
        {
            InitializeComponent();
            BindingContext = new NewItemRuleViewModel();
        }

        private void Button_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

        //private void entry2_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (entry1.Text != "" || entry2.Text != "") { butt.BackgroundColor = Color.Black; butt.TextColor = Color.White; }
        //}
    }
}