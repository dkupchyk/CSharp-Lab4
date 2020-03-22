using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Kupchyk01.Model;
using Kupchyk01.Tools.Managers;
using Kupchyk01.Tools.Navigation;

namespace Kupchyk01.ViewModel
{
    internal class UserViewModel : INotifyPropertyChanged
    {
        #region Fields
        private ObservableCollection<Person> _personsList = StationManager.DataStorage.PersonsList;
        private Person _selectedPerson;
        private int _sortIndex;
        private int _filterIndex;
        private string[] _sortList = { "FirstName", "LastName", "Email", "Birthday", "SunSign", "ChineseSign" };
        private string[] _filterList = { "FirstName", "LastName", "Email", "SunSign", "ChineseSign" };

        #endregion

        #region Commands
        private RelayCommand<object> _filterCommand;
        private RelayCommand<object> _addUserCommand;
        private RelayCommand<object> _modifyUserCommand;
        private RelayCommand<object> _deleteUserCommand;
        private RelayCommand<object> _saveCommand;
        #endregion

        #region Properties

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged();
            }
        }
       
        internal UserViewModel()
        {
            StationManager.gridVM = this;
            StationManager.ModifyVM = new ModifyUserViewModel();
        }

        public string FilterWord { get; set; }

        public int SortIndex
        {
            get => _sortIndex;
            set
            {
                _sortIndex = value;
                Update();
            }
        }

        public int FilterIndex
        {
            get => _filterIndex;
            set
            {
                _filterIndex = value;
                Update();
            }
        }

        public IEnumerable<Person> MyPersonsList
        {
            get
            {
                IEnumerable<Person> list = _personsList;
                switch (SortIndex)
                {
                    case 0: list = list.OrderBy(p => p.FirstName); break;
                    case 1: list = list.OrderBy(p => p.LastName); break;
                    case 2: list = list.OrderBy(p => p.Email); break;
                    case 3: list = list.OrderBy(p => p.DateOfBirth); break;
                    case 4: list = list.OrderBy(p => p.SighWest); break;
                    case 5: list = list.OrderBy(p => p.SighChina); break;
                }

                if (String.IsNullOrWhiteSpace(FilterWord)) return list;

                switch (FilterIndex)
                {
                    case 0: list = list.Where(p => p.FirstName.Contains(FilterWord)); break;
                    case 1: list = list.Where(p => p.LastName.Contains(FilterWord)); break;
                    case 2: list = list.Where(p => p.Email.Contains(FilterWord)); break;
                    case 3: list = list.Where(p => p.SighChina.Contains(FilterWord)); break;
                    case 4: list = list.Where(p => p.SighWest.Contains(FilterWord)); break;
                }

                return list;
            }
        }

        public IEnumerable<string> SortList => _sortList;

        public IEnumerable<string> FilterList => _filterList;

        #endregion

        #region Commands
        public RelayCommand<object> FilterCommand => _filterCommand ?? (_filterCommand = new RelayCommand<object>(o => { Update(); }));

        public RelayCommand<object> AddUserCommand => _addUserCommand ?? (_addUserCommand = new RelayCommand<object>(o => AddUserImplementation()));

        public RelayCommand<object> ModifyUserCommand => _modifyUserCommand ?? (_modifyUserCommand = new RelayCommand<object>(o => ModifyUserImplementation(), CanExecute));

        public RelayCommand<object> DeleteUserCommand => _deleteUserCommand ?? (_deleteUserCommand = new RelayCommand<object>(o => DeleteUserImplementation(), CanExecute));

        public RelayCommand<object> SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand<object>(o => SaveImplementation()));


        #endregion

        private void AddUserImplementation()
        {
            StationManager.CurrentPerson = new Person("", "", "");
            NavigationManager.Instance.Navigate(ViewType.AddUser);
        }

        private void ModifyUserImplementation()
        {
            StationManager.SelectedPerson = SelectedPerson;
            StationManager.ModifyVM._user = SelectedPerson;
            StationManager.ModifyVM.Update();
            NavigationManager.Instance.Navigate(ViewType.ModifyUser);  
        }

        private void DeleteUserImplementation()
        {
            StationManager.DataStorage.DeletePerson(SelectedPerson);
            Update();
        }

        private void SaveImplementation()
        {
            StationManager.DataStorage.SaveChanges();
        }

        private bool CanExecute(object obj) => SelectedPerson != null;

        public void Update() { OnPropertyChanged(nameof(MyPersonsList)); }

        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}