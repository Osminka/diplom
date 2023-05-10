﻿using diplom.ViewModels;
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
    public partial class webviewPage : ContentPage
    {
        //WebView webView;
        //Entry urlEntry;
        ItemsViewModel _viewModel;
        public webviewPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ItemsViewModel();
            //urlEntry = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand };

            //StackLayout stack = new StackLayout
            //{
            //    Orientation = StackOrientation.Horizontal,
            //    //Children = { button, urlEntry }
            //};
            //webView = new WebView
            //{
            //    Source = new UrlWebViewSource { Url = "https://adukar.com/images/photo/Bel_mova_5kl_Valochka_ch2_rus_bel_2019.pdf" },
            //    // или так
            //    // Source = "https://devblogs.microsoft.com/xamarin/",
            //    VerticalOptions = LayoutOptions.FillAndExpand
            //};

            //this.Content = new StackLayout { Children = { stack, webView } };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
        void button_Clicked(object sender, EventArgs e)
        {
            //webView.Source = new UrlWebViewSource { Url = urlEntry.Text };
            // или так
            // webView.Source = urlEntry.Text;
        }
    }
}