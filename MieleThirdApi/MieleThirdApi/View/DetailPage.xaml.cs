using MieleThirdApi.Model;
using MieleThirdApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MieleThirdApi.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPage : ContentPage
	{
		public DetailPage (String fabNr)
		{
			InitializeComponent ();
            BindingContext = new DetailPageViewModel(this.Navigation, fabNr);
		}

        public DetailPage()
        {
            https://docs.microsoft.com/en-us/xamarin/xamarin-forms/xaml/xaml-previewer/?pivots=windows
            InitializeComponent();
            if (DesignMode.IsDesignModeEnabled)
            {
                var vm = new DetailPageViewModel(this.Navigation);
                vm.IsBusy = false;
                BindingContext = vm;
            }

            if (!DesignMode.IsDesignModeEnabled)
            {
                // Don't run in the Previewer  
            }
        }
	}
}