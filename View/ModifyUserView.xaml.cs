using Kupchyk01.Tools.Navigation;
using Kupchyk01.ViewModel;
using System.Windows.Controls;

namespace Kupchyk01.View
{
    /// <summary>
    /// Interaction logic for ModifyUserView.xaml
    /// </summary>
    public partial class ModifyUserView : UserControl, INavigatable
    {
        public ModifyUserView()
        {
            InitializeComponent();
            DataContext = new ModifyUserViewModel(); 
        }
    }
}
