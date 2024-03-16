using ManageStaff.UI.ViewModels;
using System.Windows;


namespace ManageStaff.UI.Views
{
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();

            DataContext = new AddWindowVM();
        }
    }
}
