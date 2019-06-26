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
		public DetailPage (String details)
		{
			InitializeComponent ();
            BindingContext = new DetailPageViewModel(this.Navigation, details);
		}
	}
}