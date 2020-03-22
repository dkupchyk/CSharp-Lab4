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
    internal class AddUserViewModel: BaseViewModel, ILoaderOwner
    {

        #region Fields
        private Person _user = StationManager.CurrentPerson;
        private Visibility _visibility = Visibility.Collapsed;
        private bool _isControlEnabled = true;
        #endregion

        #region Properties 
        private string FirstName
        {
            get { return _user.FirstName; }
            set{ _user.FirstName = value; }
        }

        private string LastName
        {
            get { return _user.LastName; }
            set { _user.LastName = value; }
        }

        private string Email
        {
            get { return _user.Email; }
            set { _user.Email = value; }
        }

        private DateTime DateOfBirth
        {
            get
            {
                return _user.DateOfBirth;
            }
            set
            {
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

        internal AddUserViewModel()
        {
            LoaderManager.Instance.Initialize(this);
        }
        #endregion

        #region Commands
        private RelayCommand<object> _createUserCommand;
        private RelayCommand<object> _backToMainCommand;

        public RelayCommand<object> CreateNewUserCommand => _createUserCommand ?? (_createUserCommand = new RelayCommand<object>(o => CreateUserImplementation(), CanExecute));
        public RelayCommand<object> BackToMainCommand => _backToMainCommand ?? (_backToMainCommand = new RelayCommand<object>(o => BackToMainImplementation()));

        private bool CanExecute(object obj)
        {
            return (_user.DateOfBirth != DateTime.MinValue) && !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName) && !string.IsNullOrWhiteSpace(Email);
        }
        #endregion


        #region CommandImplementation

        private async void CreateUserImplementation(){

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

                        Person createdUser = new Person(FirstName, LastName, Email, DateOfBirth);
                        StationManager.DataStorage.AddPerson(createdUser);
                        StationManager.CurrentPerson = new Person("", "", "");
                        StationManager.gridVM.Update();
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
                MessageBox.Show("New user was successfully created!");

            });
        }

        private void BackToMainImplementation()
        {
            NavigationManager.Instance.Navigate(ViewType.Main);
        }
        #endregion
    }
}