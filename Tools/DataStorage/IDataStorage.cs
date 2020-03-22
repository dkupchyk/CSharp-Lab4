using Kupchyk01.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kupchyk01.Tools.DataStorage
{
    internal interface IDataStorage
    {
        ObservableCollection<Person> PersonsList { get; }

        void AddPerson(Person person);

        void DeletePerson(Person person);

        void SaveChanges();
    }
}
