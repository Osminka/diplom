using diplom.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace diplom.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        public string Id { get; set; }

        public ItemDetailViewModel()
        {
            DeleteCommand = new Command(OnDelete);
            CancelCommand = new Command(OnCancel);
            UpdateCommand = new Command(OnUpdate);
            this.PropertyChanged +=
                (_, __) => DeleteCommand.ChangeCanExecute();
            this.PropertyChanged +=
               (_, __) => UpdateCommand.ChangeCanExecute();
        }

        public Command DeleteCommand { get; }
        public Command UpdateCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        public static bool action;
        public async void DisplayAlertMethod()
        {
            action = await App.Current.MainPage.DisplayAlert("Внимание!", "Вы уверены, что хотите удалить запись?", "Yes", "No");

        }
        private async void OnDelete()
        {
            var item = await DataStore.GetItemAsync(itemId);
            Id = item.Id;
            Console.WriteLine(Id);
            await DataStore.DeleteItemAsync(Id);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");


        }
        private async void OnUpdate()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Description = Description
            };
            var item = await DataStore.GetItemAsync(itemId);
            Id = item.Id;
            await DataStore.UpdateItemAsync(newItem, Id);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");


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

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
