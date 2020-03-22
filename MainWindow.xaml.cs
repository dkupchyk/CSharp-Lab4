using Kupchyk01.ViewModel;
using Kupchyk01.Tools.Managers;
using Kupchyk01.Tools.Navigation;
using System.Windows;
using System.Windows.Controls;
using Kupchyk01.Tools.DataStorage;

namespace Kupchyk01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IContentOwner
    {
        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            StationManager.Initialize(new SerializedDataStorage());
            NavigationManager.Instance.Initialize(new InitializationNavigationModel(this));
            NavigationManager.Instance.Navigate(ViewType.Main);
        }
    }
}
