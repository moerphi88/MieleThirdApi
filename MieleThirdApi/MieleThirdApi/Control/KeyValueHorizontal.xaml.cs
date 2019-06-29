using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MieleThirdApi.Control
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class KeyValueHorizontal : ContentView
	{
        // KeyText
        public static readonly BindableProperty KeyTextProperty = BindableProperty.Create(
                                                         propertyName: "KeyText",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(KeyValueHorizontal),
                                                         defaultValue: "",
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: KeyTextPropertyChanged);

        public string KeyText
        {
            get { return base.GetValue(KeyTextProperty).ToString(); }
            set { base.SetValue(KeyTextProperty, value); }
        }

        private static void KeyTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (KeyValueHorizontal)bindable;
            control.keytext.Text = newValue.ToString();
        }

        //ValueText
        public static readonly BindableProperty ValueTextProperty = BindableProperty.Create(
                                                 propertyName: "ValueText",
                                                 returnType: typeof(string),
                                                 declaringType: typeof(KeyValueHorizontal),
                                                 defaultValue: "",
                                                 defaultBindingMode: BindingMode.TwoWay,
                                                 propertyChanged: ValueTextPropertyChanged);

        public string ValueText
        {
            get { return base.GetValue(ValueTextProperty).ToString(); }
            set { base.SetValue(ValueTextProperty, value); }
        }

        private static void ValueTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (KeyValueHorizontal)bindable;
            control.valuetext.Text = newValue.ToString();
        }

        public string BackgroundColor
        {
            get { return base.GetValue(ValueTextProperty).ToString(); }
            set { base.SetValue(ValueTextProperty, value); }
        }

        public KeyValueHorizontal ()
		{
			InitializeComponent ();
		}
	}
}