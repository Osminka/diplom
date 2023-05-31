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

        //private void entry2_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (entry1.Text != "" || entry2.Text != "") { butt.BackgroundColor = Color.Black; butt.TextColor = Color.White; }
        //}
    }
}