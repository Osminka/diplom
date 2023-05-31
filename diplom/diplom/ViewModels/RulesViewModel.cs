using diplom.Models;
using diplom.ViewModels;
using diplom.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

using Xamarin.Forms;

namespace diplom.ViewModels
{
    public class RulesViewModel : BaseViewModel2
    {
        private Item2 _selectedItem;

        public ObservableCollection<Item2> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command<Item2> ItemTapped { get; }
        public Command AddItemCommand { get; }
        public RulesViewModel()
        {
            Title = "Правілы";
            Items = new ObservableCollection<Item2>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item2>(OnItemSelected); //ОБРАТИТЬ ВНИМАНИЕ
            AddItemCommand = new Command(OnAddItem);
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore2.GetItemsAsync(true);
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

        public Item2 SelectedItem
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
            await Shell.Current.GoToAsync(nameof(NewItemRulePage));
        }
        async void OnItemSelected(Item2 item)
        {
            bool name = App.AdmOrNo;
            if (item == null)
                return;
            if (name == true)
            {
                await Shell.Current.GoToAsync($"{nameof(ItemDetailRulePage)}?{nameof(ItemDetailRuleViewModel.ItemId)}={item.Id}");

            }
            else
            {
                //await Shell.Current.Navigation.PushAsync($"{nameof(ItemPageDinamic)}?{nameof(DinamicDetailViewModel.ItemId)}={item.Id}");
                await Shell.Current.GoToAsync($"{nameof(RuleItemPageDinamic)}?{nameof(DinamicDetailRuleViewModel.ItemId)}={item.Id}");

            }
        }
    }
}