using System.Windows;
using System.Windows.Controls;

namespace KMA.ProgrammingInCSharp.LW4Polishchuk.Controls
{
    public partial class NamedCheckBox : UserControl
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register
        (
            "Label",
            typeof(string),
            typeof(NamedCheckBox),
            new PropertyMetadata(null)
        );
        
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register
        (
            "IsChecked",
            typeof(bool?),
            typeof(NamedCheckBox),
            new PropertyMetadata(null)
        );
        
        public new static readonly DependencyProperty MarginProperty = DependencyProperty.Register
        (
            "Margin",
            typeof(string),
            typeof(NamedCheckBox),
            new PropertyMetadata(null)
        ); 

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public bool? IsChecked
        {
            get => (bool?)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public new string Margin
        {
            get => (string)GetValue(MarginProperty);
            set => SetValue(MarginProperty, value);
        }
        
        public NamedCheckBox()
        {
            InitializeComponent();
        }
    }
}