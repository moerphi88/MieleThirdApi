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
    public partial class FaceliftCellGrid : ContentView
    {
        //https://mindofai.github.io/Creating-Custom-Controls-with-Bindable-Properties-in-Xamarin.Forms/
        //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/xaml/bindable-properties
        // KeyText
        public static readonly BindableProperty KeyTextProperty = BindableProperty.Create(
                                                         propertyName: "KeyText",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(FaceliftCellGrid),
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
            var control = (FaceliftCellGrid)bindable;
            control.LableText.Text = newValue.ToString();
        }

        // ValueText
        public static readonly BindableProperty ValueTextProperty = BindableProperty.Create(
                                                         propertyName: "ValueText",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(FaceliftCellGrid),
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
            var control = (FaceliftCellGrid)bindable;
            control.valueText.Text = newValue.ToString();
        }

        // BoxColor
        public static readonly BindableProperty BoxColorProperty = BindableProperty.Create(
                                                         propertyName: "BoxColor",
                                                         returnType: typeof(Color),
                                                         declaringType: typeof(FaceliftCellGrid),
                                                         defaultValue: Color.AliceBlue,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: BoxColorPropertyChanged);

        public Color BoxColor
        {
            get { return (Color)base.GetValue(BoxColorProperty); }
            set { base.SetValue(BoxColorProperty, value); }
        }

        private static void BoxColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FaceliftCellGrid)bindable;
            control.box.BackgroundColor = (Color)newValue;
        }


        public FaceliftCellGrid()
        {
            InitializeComponent();
        }
    }
}