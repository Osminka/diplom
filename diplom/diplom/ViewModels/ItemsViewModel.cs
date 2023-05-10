using Android.Content.Res;
using diplom.Models;
using diplom.Services;
using diplom.Views;
using Newtonsoft.Json;
using Org.W3c.Dom;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace diplom.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Размоўнікі";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected); //ОБРАТИТЬ ВНИМАНИЕ

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                //var json = string.Empty;

                //var context = Android.App.Application.Context;
                //AssetManager assets = context.Assets;
                //var assembly = typeof(App).GetTypeInfo().Assembly;
                ////Stream stream = assembly.GetManifestResourceStream("diplom.file.json");
                //using (var reader = new System.IO.StreamReader(assets.Open("file.json")))
                //{
                //    json = reader.ReadToEnd();
                //    //root1 = JsonConvert.DeserializeObject<Root>(json);
                //}
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            string name = "admin";
            if (item == null)
                return;
            if (name == "admin")
            {
                await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");

            }
            else
            {
                await Shell.Current.GoToAsync($"{nameof(ItemPageDinamic)}?{nameof(DinamicDetailViewModel.ItemId)}={item.Id}");

            }
            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}