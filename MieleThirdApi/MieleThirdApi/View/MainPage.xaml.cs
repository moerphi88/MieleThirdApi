using MieleThirdApi.Model;
using MieleThirdApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MieleThirdApi.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

           // https://montemagno.com/xamarinforms-xaml-previewer-design-time-data/
            if (DesignMode.IsDesignModeEnabled)
            {
                var vm = new MainPageViewModel(this.Navigation);
                vm.IsBusy = true;
                vm.DeviceList = new ObservableCollection<DevicelistItem>() { new DevicelistItem()
                {   Name = "Lorem ipsum bla bla bla",
                    IconUri = "icon.png",
                    EndeZeit = "19:23",
                    ProgressBarValue = 0.4,
                    Status = "Running"                
                }
                };
                BindingContext = vm;
            }

            if (!DesignMode.IsDesignModeEnabled)
            {
                BindingContext = new MainPageViewModel(this.Navigation);
            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null)
            {
                Navigation.PushAsync(new DetailPage("fabNr"));
            }
        }
    }
}
