using ManageStaff.UI.ViewModels;
using System.Windows;


namespace ManageStaff.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowVM();
        }
    }
}