using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
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
        private RelayCommand<object> _sortCommand;
        private RelayCommand<object> _filterCommand;
        private RelayCommand<object> _addUserCommand;
        private RelayCommand<object> _modifyUserCommand;
        private RelayCommand<object> _deleteUserCommand;
        private RelayCommand<object> _saveCommand;
        #endregion

        #region Properties

        public Person SelectedPerson
        {
            get { return _selectedPerson; }
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
            _personsList = new ObservableCollection<Person>(_personsList.OrderBy(p => p.FirstName));
        }

        public string FilterWord { get; set; }

        public int SortIndex
        {
            get { return _sortIndex; }
            set
            {
                _sortIndex = value;
                OnPropertyChanged();
            }
        }

        public int FilterIndex
        {
            get { return _filterIndex; }
            set
            {
                _filterIndex = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Person> MyPersonsList
        {
            get {
                return _personsList;
            }
            set
            {
                _personsList = value;
                OnPropertyChanged(nameof(MyPersonsList));
            }
        }

        public IEnumerable<string> SortList => _sortList;

        public IEnumerable<string> FilterList => _filterList;

        #endregion

        #region Commands
        public RelayCommand<object> SortCommand => _sortCommand ?? (_sortCommand = new RelayCommand<object>(o => SortImplementation()));

        public RelayCommand<object> FilterCommand => _filterCommand ?? (_filterCommand = new RelayCommand<object>(o => { FilterImplementation(); }));

        public RelayCommand<object> AddUserCommand => _addUserCommand ?? (_addUserCommand = new RelayCommand<object>(o => AddUserImplementation()));

        public RelayCommand<object> ModifyUserCommand => _modifyUserCommand ?? (_modifyUserCommand = new RelayCommand<object>(o => ModifyUserImplementation(), CanExecute));

        public RelayCommand<object> DeleteUserCommand => _deleteUserCommand ?? (_deleteUserCommand = new RelayCommand<object>(o => DeleteUserImplementation(), CanExecute));

        public RelayCommand<object> SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand<object>(o => SaveImplementation()));


        #endregion
        private void SortImplementation()
        {
            switch (SortIndex)
            {
                case 0:
                    _personsList = new ObservableCollection<Person>(_personsList.OrderBy(p => p.FirstName));
                    break;
                case 1:
                    _personsList = new ObservableCollection<Person>(_personsList.OrderBy(p => p.LastName));
                    break;
                case 2:
                    _personsList = new ObservableCollection<Person>(_personsList.OrderBy(p => p.Email));
                    break;
                case 3:
                    _personsList = new ObservableCollection<Person>(_personsList.OrderBy(p => p.DateOfBirth));
                    break;
                case 4:
                    _personsList = new ObservableCollection<Person>(_personsList.OrderBy(p => p.SighWest));
                    break;
                case 5:
                    _personsList = new ObservableCollection<Person>(_personsList.OrderBy(p => p.SighChina));
                    break;
            }
            OnPropertyChanged(nameof(MyPersonsList));
            _personsList = StationManager.DataStorage.PersonsList;
        }

        private void FilterImplementation()
        {
            if (String.IsNullOrWhiteSpace(FilterWord)) return;

            switch (FilterIndex)
            {
                case 0:
                    _personsList = new ObservableCollection<Person>(_personsList.Where(p => p.FirstName.Contains(FilterWord)));
                    break;
                case 1:
                    _personsList = new ObservableCollection<Person>(_personsList.Where(p => p.LastName.Contains(FilterWord)));
                    break;
                case 2:
                    _personsList = new ObservableCollection<Person>(_personsList.Where(p => p.Email.Contains(FilterWord)));
                    break;
                case 3:
                    _personsList = new ObservableCollection<Person>(_personsList.Where(p => p.SighChina.Contains(FilterWord)));
                    break;
                case 4:
                    _personsList = new ObservableCollection<Person>(_personsList.Where(p => p.SighWest.Contains(FilterWord)));
                    break;
            }
            OnPropertyChanged(nameof(MyPersonsList));
            _personsList = StationManager.DataStorage.PersonsList;
        }

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
            MyPersonsList = StationManager.DataStorage.PersonsList;
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