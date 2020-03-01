using System.Windows;
using System.Windows.Controls;

namespace KMA.ProgrammingInCSharp.LW2Polishchuk.Controls
{
    public partial class NamedTextBox : UserControl
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register
        (
            "Label",
            typeof(string),
            typeof(NamedTextBox),
            new PropertyMetadata(null)
        );
        
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register
        (
            "Text",
            typeof(string),
            typeof(NamedTextBox),
            new PropertyMetadata(null)
        );
        
        public new static readonly DependencyProperty MarginProperty = DependencyProperty.Register
        (
            "Margin",
            typeof(string),
            typeof(NamedTextBox),
            new PropertyMetadata(null)
        ); 

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public new string Margin
        {
            get => (string)GetValue(MarginProperty);
            set => SetValue(MarginProperty, value);
        }
        
        public NamedTextBox()
        {
            InitializeComponent();
        }
    }
}