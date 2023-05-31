using diplom.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace diplom.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class DinamicDetailRuleViewModel : BaseViewModel2
    {
        private string itemId;
        private string text;
        private string name;
        public string Id { get; set; }

        public DinamicDetailRuleViewModel()
        {
            Title = "Правілы";
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
            var item = await DataStore2.GetItemAsync(itemId);
            Id = item.Id;
            await DataStore2.DeleteItemAsync(Id);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");


        }
        private async void OnUpdate()
        {
            Item2 newItem = new Item2()
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Name = Name
            };
            var item = await DataStore2.GetItemAsync(itemId);
            Id = item.Id;
            await DataStore2.UpdateItemAsync(newItem, Id);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");


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
                var item = await DataStore2.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Name = item.Name;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}