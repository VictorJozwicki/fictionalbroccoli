using Xamarin.Forms;

namespace fictionalbroccoli
{
    public partial class BrocoRegisterAdd : AbsoluteLayout
    {
        public static readonly BindableProperty FooProperty =
            BindableProperty.Create(nameof(Foo), typeof(string), typeof(BrocoRegisterAdd, null));

        public BrocoRegisterAdd()
        {
            InitializeComponent();
        }

        public string Foo
        {
            get => (string)GetValue(FooProperty);
            set => SetValue(FooProperty, value);
        }
    }
}
