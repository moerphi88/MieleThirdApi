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
        private MainPageViewModel vm;
        public MainPage()
        {
            InitializeComponent();

           // https://montemagno.com/xamarinforms-xaml-previewer-design-time-data/
            if (DesignMode.IsDesignModeEnabled)
            {
                vm = new MainPageViewModel(this.Navigation);
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
                vm = new MainPageViewModel(this.Navigation);
                BindingContext = vm;
            }
        }

        protected override void OnAppearing()
        {
            vm.GetDeviceList();
            base.OnAppearing();
        }
    }
}
