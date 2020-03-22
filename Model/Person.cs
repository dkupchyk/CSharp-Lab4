using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Kupchyk01.Exceptions;

namespace Kupchyk01.Model
{
    [Serializable]
    class Person: INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _dateOfBirth;
        private string _sighChina;
        private string _sighWest;
        private int _age;
        private bool _isAdult;
        private bool _isBirthday;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public string SighChina
        {
            get { return _sighChina; }
            set
            {
                _sighChina = value;
                OnPropertyChanged();
            }
        }

        public string SighWest
        {
            get { return _sighWest; }
            set
            {
                _sighWest = value;
                OnPropertyChanged();
            }
        }
        public bool IsAdult
        {
            get { return _isAdult; }
            set
            {
                _isAdult = value;
                OnPropertyChanged();
            }
        }
        public bool IsBirthday
        {
            get { return _isBirthday; }
            set
            {
                _isBirthday = value;
                OnPropertyChanged();
            }
        }

        public Person(string firstNameP, string lastNameP, string emailP, DateTime dateOfBirthP)
        {
            FirstName = firstNameP;
            LastName = lastNameP;
            Email = emailP;
            DateOfBirth = dateOfBirthP;
            Age = (DateTime.Today - dateOfBirthP).Days / 365;
            IsAdult = CalculateIsAdult();
            IsBirthday = CalculateIsBirthdayToday();
            SighWest = CalcWesternHorosc();
            SighChina = CalcChineseHorosc();
        }

        public Person(string firstNameP, string lastNameP, string emailP)
        {
            FirstName = firstNameP;
            LastName = lastNameP;
            Email = emailP;
            IsAdult = CalculateIsAdult();
            IsBirthday = CalculateIsBirthdayToday();
            SighWest = CalcWesternHorosc();
            SighChina = CalcChineseHorosc();
        }

        public Person(string firstNameP, string lastNameP, DateTime dateOfBirthP)
        {
            FirstName = firstNameP;
            LastName = lastNameP;
            DateOfBirth = dateOfBirthP;
            Age = (DateTime.Today - dateOfBirthP).Days / 365;
            IsAdult = CalculateIsAdult();
            IsBirthday = CalculateIsBirthdayToday();
            SighWest = CalcWesternHorosc();
            SighChina = CalcChineseHorosc();
        }

       
        public string CalcWesternHorosc()
        {
            int month = (Convert.ToDateTime(DateOfBirth)).Month;
            int day = (Convert.ToDateTime(DateOfBirth)).Day;
            int index = 0;

            switch (month)
            {
                case 02 when day >= 19:
                case 03 when day <= 20:
                    index = 1;
                    break;
                case 03 when day >= 21:
                case 04 when day <= 19:
                    index = 2;
                    break;
                case 04 when day >= 20:
                case 05 when day <= 20:
                    index = 3;
                    break;
                case 05 when day >= 21:
                case 06 when day <= 20:
                    index = 4;
                    break;
                case 06 when day >= 21:
                case 07 when day <= 22:
                    index = 5;
                    break;
                case 07 when day >= 23:
                case 08 when day <= 22:
                    index = 6;
                    break;
                case 08 when day >= 23:
                case 09 when day <= 22:
                    index = 7;
                    break;
                case 09 when day >= 23:
                case 10 when day <= 22:
                    index = 8;
                    break;
                case 10 when day >= 23:
                case 11 when day <= 21:
                    index = 9;
                    break;
                case 11 when day >= 22:
                case 12 when day <= 21:
                    index = 10;
                    break;
                case 12 when day >= 22:
                case 01 when day <= 19:
                    index = 11;
                    break;
            }
            return Enum.GetName(typeof(Enums.WestSignsEnum), index);
        }

        public string CalcChineseHorosc()
        {
            return Enum.GetName(typeof(Enums.ChinSignsEnum), ((Convert.ToDateTime(DateOfBirth)).Year - 4) % 12);
        }

        public bool CalculateIsBirthdayToday()
        {
            if (DateOfBirth == null) return false;
            return ((Convert.ToDateTime(DateOfBirth)).Day == DateTime.Today.Day && (Convert.ToDateTime(DateOfBirth)).Month == DateTime.Today.Month);
        }

        public bool CalculateIsAdult() 
        {
            if (DateOfBirth == null) return false;
            return ((DateTime.Today - Convert.ToDateTime(DateOfBirth)).Days / 365) >= 18 ? true : false;
        }


        public bool ValidatePerson()
        {
            if ((Convert.ToDateTime(DateOfBirth)).Year < DateTime.Today.Year - 135)
            {
                throw new PastBirthdayException(FirstName, LastName);
            }
            else if (DateOfBirth > DateTime.Today)
            {
                throw new FutureBirthdayException(FirstName, LastName);
            }
            else if (!(new EmailAddressAttribute().IsValid(Email)))
            {
                throw new IncorrectEmailException(FirstName, LastName);
            }
            else
            {
                return true;
            }
        }

        #region OnPropertyChanged
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}