using diplom.Models;
using diplom.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace diplom.ViewModels
{
    public class NewItemRuleViewModel : BaseViewModel2
    {
        private string text;
        private string name;

        public NewItemRuleViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(name);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
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
            Item2 newItem = new Item2()
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Name = Name
                
            };

            await DataStore2.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
