using Kupchyk01.Tools.Navigation;
using Kupchyk01.ViewModel;
using System.Windows.Controls;

namespace Kupchyk01.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl, INavigatable
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new UserViewModel();
        }

    }
}
