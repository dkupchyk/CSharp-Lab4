using Kupchyk01.Exceptions;
using Kupchyk01.Model;
using Kupchyk01.Tools;
using Kupchyk01.Tools.Managers;
using Kupchyk01.Tools.Navigation;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Kupchyk01.ViewModel
{
    class ModifyUserViewModel: BaseViewModel, ILoaderOwner
    {

        #region Fields
        public Person _user = StationManager.SelectedPerson;
        private Visibility _visibility = Visibility.Collapsed;
        private bool _isControlEnabled = true;
        #endregion

        #region Properties 

        public string FirstName
        {
            get { return _user.FirstName; }
            set
            {
                _user.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _user.LastName; }
            set {
                _user.LastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _user.Email; }
            set {
                _user.Email = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfBirth
        {
            get{ return _user.DateOfBirth; }
            set{
                _user.DateOfBirth = value;
            }
        }

        public Visibility LoaderVisibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged();
            }
        }

        public bool IsControlEnabled
        {
            get { return _isControlEnabled; }
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }

        internal ModifyUserViewModel()
        {
            LoaderManager.Instance.Initialize(this);
            StationManager.ModifyVM = this;
        }
        #endregion

        #region Commands
        private RelayCommand<object> _saveChangesCommand;
        private RelayCommand<object> _backToMainCommand;

        public RelayCommand<object> SaveChangesCommand => _saveChangesCommand ?? (_saveChangesCommand = new RelayCommand<object>(o => SaveChangesImplementation(), CanExecute));

        public RelayCommand<object> BackToMainCommand => _backToMainCommand ?? (_backToMainCommand = new RelayCommand<object>(o => BackToMainImplementation()));

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(_user.DateOfBirth.ToString()) && !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName) && !string.IsNullOrWhiteSpace(Email);
        }
        #endregion

        #region CommandImplementation

        private async void SaveChangesImplementation()
        {

            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                try
                {
                    if (_user.ValidatePerson())
                    {
                        if (_user.IsBirthday == "True")
                        {
                            MessageBox.Show("Happy birthday!\nHere’s to a bright,\nhealthy and exciting future!");
                        }
                        _user.Age = (DateTime.Today - DateOfBirth).Days / 365;
                        _user.IsAdult = _user.CalculateIsAdult();
                        _user.IsBirthday = _user.CalculateIsBirthdayToday();
                        _user.SighChina = _user.CalcChineseHorosc();
                        _user.SighWest = _user.CalcWesternHorosc();

                        StationManager.DataStorage.SaveChanges();
                        StationManager.gridVM.Update();
                        StationManager.CurrentPerson = new Person("", "", "");
                    }
                }
                catch (Exception ex)
                {
                    if (ex is PastBirthdayException || ex is FutureBirthdayException || ex is IncorrectEmailException)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    throw;
                }
                Thread.Sleep(1000);
                LoaderManager.Instance.HideLoader();
                MessageBox.Show("User was successfully modified!");
            });
        }

        private void BackToMainImplementation()
        {
            NavigationManager.Instance.Navigate(ViewType.Main);
        }
        #endregion
        
        public void Update() {
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(DateOfBirth));
        }

    }
}