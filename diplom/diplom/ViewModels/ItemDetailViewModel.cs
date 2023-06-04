using diplom.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Android.Provider.UserDictionary;

namespace diplom.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        private string wordRus;
        private string varCorr;
        private string var1;
        private string var2;
        private string imageSource;
        public string Id { get; set; }

        public ItemDetailViewModel()
        {
            Title = "Размоўнікі";
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
                Description = Description,
                WordRus = WordRus,
                VariantCorrect = VariantCorrect,
                Variant1 = Variant1,
                Variant2 = Variant2,
                ImageSource = ImageSource,
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
            set => SetProperty(ref var1, value);
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
                WordRus = item.WordRus;
                VariantCorrect = item.VariantCorrect;
                Variant1 = item.Variant1;
                Variant2 = item.Variant2;
                ImageSource = item.ImageSource;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
