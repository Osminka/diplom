using diplom.Models;
using diplom.ViewModels;
using diplom.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using static Android.Graphics.ImageDecoder;

namespace diplom.ViewModels
{
    public class NewLitViewModel : BaseViewModelLit
    {
        private string name;
        private string source;
        private string imageSource;

        public NewLitViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            //NewItemPage newItemPage = new NewItemPage();
            //newItemPage.MethodButt();
            return !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(source) && !String.IsNullOrWhiteSpace(imageSource);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Source
        {
            get => source;
            set => SetProperty(ref source, value);
        }
        public string ImageSource
        {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            ItemLit newItem = new ItemLit()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Source = Source,
                ImageSource = ImageSource
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}

