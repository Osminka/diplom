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
    public class LitViewModel : BaseViewModelLit
    {
        private ItemLit _selectedItem;

        public ObservableCollection<ItemLit> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<ItemLit> ItemTapped { get; }

        public LitViewModel()
        {
            Title = "Літаратура";
            Items = new ObservableCollection<ItemLit>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<ItemLit>(OnItemSelected); //ОБРАТИТЬ ВНИМАНИЕ

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

        public ItemLit SelectedItem
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
            await Shell.Current.GoToAsync(nameof(NewLitPage));
        }

        async void OnItemSelected(ItemLit item)
        {
            //bool name = App.AdmOrNo;
            //if (item == null)
            //    return;
            //if (name == true)
            //{
            //    await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");

            //}
            //else
            //{
            bool name = App.AdmOrNo;
            if (item == null)
                return;
            if (name == true)
            {
                await Shell.Current.GoToAsync($"{nameof(LitPageDinamic)}?{nameof(DinamicDetailViewModelLit.ItemId)}={item.Id}");

            }
            else
            {
                //await Shell.Current.Navigation.PushAsync($"{nameof(ItemPageDinamic)}?{nameof(DinamicDetailViewModel.ItemId)}={item.Id}");
                await Shell.Current.GoToAsync($"{nameof(LitDetailPage)}?{nameof(LitDetailViewModel.ItemId)}={item.Id}");

            }
            //await Shell.Current.Navigation.PushAsync($"{nameof(ItemPageDinamic)}?{nameof(DinamicDetailViewModel.ItemId)}={item.Id}");


            //}
        }
    }
}