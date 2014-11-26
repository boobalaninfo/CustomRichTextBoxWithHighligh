using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

namespace CustomRichTextBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        const string EnableEdit = "Enable Edit";
        const string DisableEdit = "Disable Edit";

        private bool _isRTBEnabled;

        public bool IsRTBEnabled { get { return _isRTBEnabled; } set { _isRTBEnabled = value; OnPropertyChanged("IsRTBEnabled");
        } }
        public MainWindow()
        {
            InitializeComponent();
            BtnEnable.Content = EnableEdit;
            IsRTBEnabled = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var title = BtnEnable.Content;
            if (title.Equals(EnableEdit))
            {
                BtnEnable.Content = DisableEdit;
                BtnEnable.Background = Brushes.Red;
                IsRTBEnabled = true;
            }
            else
            {
                IsRTBEnabled = false;
                BtnEnable.Content = EnableEdit;
                BtnEnable.Background = Brushes.Green;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
