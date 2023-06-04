using diplom.Models;
using diplom.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace diplom.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string text;
        private string description;
        private string wordRus;
        private string varCorr;
        private string var1;
        private string var2;
        private string imageSource;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public string WordRus
        {
            get => wordRus;
            set => SetProperty(ref wordRus, value);
        }
        public string VariantCorrect
        {
            get => varCorr;
            set => SetProperty(ref varCorr, value);
        }
        public string Variant1
        {
            get => var1;
            set => SetProperty(ref var2, value);
        }
        public string Variant2
        {
            get => var2;
            set => SetProperty(ref var2, value);
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
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Description = Description,
                WordRus = WordRus,
                VariantCorrect = VariantCorrect,
                Variant1 = Variant1,
                Variant2 = Variant2,
                ImageSource = ImageSource,
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
