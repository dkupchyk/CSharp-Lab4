using System;
using Kupchyk01.View;

namespace Kupchyk01.Tools.Navigation
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner) {}
   
        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.ModifyUser:
                    ViewsDictionary.Add(viewType, new ModifyUserView());
                    break;
                case ViewType.Main:
                    ViewsDictionary.Add(viewType, new MainView());
                    break;
                case ViewType.AddUser:
                    ViewsDictionary.Add(viewType, new AddUserView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
