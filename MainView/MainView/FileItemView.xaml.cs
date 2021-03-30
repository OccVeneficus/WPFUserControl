using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainView
{
    /// <summary>
    /// Interaction logic for FileItemView.xaml
    /// </summary>
    public partial class FileItemView : UserControl
    {
        public FileItemView()
        {
            InitializeComponent();
        }

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty,value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text),typeof(string),typeof(FileItemView));

        public ControlTemplate ValidationTemplate
        {
            get => (ControlTemplate)GetValue(ValidationTemplateProperty);
            set => SetValue(ValidationTemplateProperty, value);
        }

        public static readonly DependencyProperty ValidationTemplateProperty =
            DependencyProperty.Register(nameof(ValidationTemplate)
                , typeof(ControlTemplate)
                , typeof(FileItemView)
                , new FrameworkPropertyMetadata(new ControlTemplate(), OnValidationTemplateChanged));

        private static void OnValidationTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                FileItemView control = (FileItemView)d;
                Validation.SetErrorTemplate(control.ControlGrid, (ControlTemplate)e.NewValue);
            }
        }
    }
}
