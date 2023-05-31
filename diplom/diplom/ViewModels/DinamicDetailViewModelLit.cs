using diplom.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Android.Graphics.ImageDecoder;

namespace diplom.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class DinamicDetailViewModelLit : BaseViewModelLit
    {
        private string itemId;
        private string name;
        private string source;
        private string imageSource;
        public string Id { get; set; }

        public DinamicDetailViewModelLit()
        {
            Title = "Літаратура";
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
            ItemLit newItem = new ItemLit()
            {
                Id = Guid.NewGuid().ToString(),
                Source = Source,
                Name = Name,
                ImageSource = ImageSource
            };
            var item = await DataStore.GetItemAsync(itemId);
            Id = item.Id;
            await DataStore.UpdateItemAsync(newItem, Id);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");


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
                Name = item.Name;
                Source = item.Source;
                ImageSource = item.ImageSource;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}