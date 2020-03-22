using System;
using System.Windows;
using System.Windows.Controls;
using Kupchyk01.Model;
using Kupchyk01.Tools.DataStorage;
using Kupchyk01.ViewModel;

namespace Kupchyk01.Tools.Managers
{
    internal static class StationManager
    {
        public static event Action StopThreads;
        internal static Person CurrentPerson { get; set; }
        internal static Person SelectedPerson { get; set; }
        internal static DataGrid PersonGrid { get; set; }
        internal static ModifyUserViewModel ModifyVM { get; set; }
        internal static UserViewModel gridVM { get; set; }
        internal static IDataStorage DataStorage { get; private set; }
        internal static void Initialize(IDataStorage dataStorage)
        {
            DataStorage = dataStorage;
        }

        internal static void CloseApp()
        {
            MessageBox.Show("Closing the app...");
            StopThreads?.Invoke();
            Environment.Exit(1);
        }

    }
}
