using System.Windows.Controls;
using Kupchyk01.Tools.Navigation;
using Kupchyk01.ViewModel;

namespace Kupchyk01.View
{
    /// <summary>
    /// Interaction logic for AddUserView.xaml
    /// </summary>
    public partial class AddUserView : UserControl, INavigatable
    {
        public AddUserView()
        {
            InitializeComponent();
            DataContext = new AddUserViewModel();
        }
    }
}
