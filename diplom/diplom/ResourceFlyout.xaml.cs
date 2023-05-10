using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace diplom
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResourceFlyout : ContentPage
    {
        public ListView ListView;

        public ResourceFlyout()
        {
            InitializeComponent();

            BindingContext = new ResourceFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class ResourceFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<ResourceFlyoutMenuItem> MenuItems { get; set; }

            public ResourceFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<ResourceFlyoutMenuItem>(new[]
                {
                    new ResourceFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new ResourceFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new ResourceFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new ResourceFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new ResourceFlyoutMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}