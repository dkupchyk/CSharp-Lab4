using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Kupchyk01.Model;
using Kupchyk01.Tools.Managers;

namespace Kupchyk01.Tools.DataStorage
{
    internal class SerializedDataStorage : IDataStorage
    {
        private readonly ObservableCollection<Person> _persons;

        internal SerializedDataStorage()
        {
            try
            {
                _persons = SerializationManager.Deserialize<ObservableCollection<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _persons = new ObservableCollection<Person>();
                FillPersons();
            }
        }

        public void AddPerson(Person person)
        {
            _persons.Add(person);
            SaveChanges();
        }

        public void DeletePerson(Person person)
        {
            _persons.Remove(person);
            SaveChanges();
        }

        public ObservableCollection<Person> PersonsList => _persons;

        public void SaveChanges()
        {
            SerializationManager.Serialize(_persons, FileFolderHelper.StorageFilePath);
        }
        
        private void FillPersons()
        {
            Random ran = new Random();
            string[] firstNames = { "Владислав", "Крістіна", "Максим", "Владислав", "Іван", "Роман", "Євгеній", "Владислав", "Валентин", "Олексій", "Олександра", "Олександр", "Людмила", "Олександр", "Валерія", "Іван", "Михайло", "Ярослав", "Вадим", "Роман", "Тарас", "Андрій", "Ілля", "Михайло", "Єва", "Серафима", "Олександр", "Ярослав", "Марія", "Вікторія", "Ілля", "Анастасія", "Микола", "Ангеліна", "Дарія", "Павло", "Мілена", "Марія", "Наталія", "Іван", "Марина", "Артем", "Михайло", "Андрій", "Марина", "Дмитро", "Владислава", "Катерина", "Роман", "Дарина" };
            string[] lastNames = { "Андронік", "Бехер", "Білан", "Благов", "Василів", "Возбранний", "Войцеховський", "Галиць", "Гурін", "Данилюк", "Дорошенко", "Дубчак", "Єлгешіна", "Ємельянов", "Задонцева", "Іщенко", "Кобєлєв", "Костенко", "Кочмар", "Красюк", "Крещенко", "Курбатов", "Курочкін", "Куценко", "Лабунська", "Лопухіна", "Ляшенко", "Ляшенко", "Маймескул", "Мацюк", "Милаш", "Місюра", "Мощицький", "Настенко", "Ніколаюк", "Палійчук", "Печура", "Самовол", "Смальченко", "Тимошенко", "Тютюн", "Устілов", "Федюченко", "Філенко", "Харченко", "Хоменко", "Шевченко", "Шекета", "Щибрик", "Яськова" };
            string[] emails = { "andronik.vladyslav", "bekher.kristina", "bilan.maksym", "blahov.vladyslav", "vasyliv.ivan", "vozbrannyy.roman", "voytsekhovskyy.yevheniy", "halyts.vladyslav", "hurin.valentyn", "danylyuk.oleksiy", "doroshenko.oleksandra", "dubchak.oleksandr", "yelheshina.lyudmyla", "yemelyanov.oleksandr", "zadontseva.valeriya", "ishchenko.ivan", "kobyelyev.mykhaylo", "kostenko.yaroslav", "kochmar.vadym", "krasyuk.roman", "kreshchenko.taras", "kurbatov.andriy", "kurochkin.illya", "kutsenko.mykhaylo", "labunska.yeva", "lopukhina.serafyma", "lyashenko.oleksandr", "lyashenko.yaroslav", "maymeskul.mariya", "matsyuk.viktoriya", "mylash.illya", "misyura.anastasiya", "moshchytskyy.mykola", "nastenko.anhelina", "nikolayuk.dariya", "paliychuk.pavlo", "pechura.milena", "samovol.mariya", "smalchenko.nataliya", "tymoshenko.ivan", "tyutyun.maryna", "ustilov.artem", "fedyuchenko.mykhaylo", "filenko.andriy", "kharchenko.maryna", "khomenko.dmytro", "shevchenko.vladyslava", "sheketa.kateryna", "shchybryk.roman", "yaskova.daryna", };
            
            for (int i = 0; i < 50; i++)
            {
                DateTime birthday = new DateTime(ran.Next(1990, 2005), ran.Next(1, 12), ran.Next(1, 28));
                AddPerson(new Person(firstNames[i], lastNames[i], emails[i] + "@gmail.com", birthday));
            }
        }
    }
}
