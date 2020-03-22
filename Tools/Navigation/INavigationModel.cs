namespace Kupchyk01.Tools.Navigation
{
    internal enum ViewType
    {
        Main,
        AddUser,
        SelectBy,
        ModifyUser,
        DeleteUser,
        Save
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
