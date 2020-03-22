using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using KMA.ProgrammingInCSharp.LW4Polishchuk.Manangers;
using KMA.ProgrammingInCSharp.LW4Polishchuk.Models;
using KMA.ProgrammingInCSharp.LW4Polishchuk.ViewModels;

namespace KMA.ProgrammingInCSharp.LW4Polishchuk.Services
{
    internal static class UsersProvider
    {
        public static ObservableCollection<PersonViewModel> GetUsers()
        {
            try
            {
                var users =  SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
                return new ObservableCollection<PersonViewModel>(users.Select(u => new PersonViewModel(u)));
            }
            catch (FileNotFoundException)
            {
                var users = GenerateUsers();
                SerializationManager.Serialize(users, FileFolderHelper.StorageFilePath);
                return new ObservableCollection<PersonViewModel>(users.Select(u => new PersonViewModel(u)));
            }
        }

        public static void SaveUsers(ObservableCollection<PersonViewModel> models)
        {
            SerializationManager.Serialize(models.Select(m => m.Person).ToList(), FileFolderHelper.StorageFilePath);
        }
        
        private static List<Person> GenerateUsers()
        {
            var names = new[]
            {
                "James", "David", "Christopher", "George", "Ronald",
                "Mary", "Jennifer", "Lisa", "Sandra", "Michelle",
                "John", "Richard", "Daniel", "Kenneth", "Anthony",
                "Linda", "Susan", "Karen", "Carol", "Sarah",
                "Robert", "Charles", "Paul", "Steven", "Kevin",
                "Linda", "Susan", "Karen", "Carol", "Sarah",
                "Michael", "Joseph", "Mark", "Edward", "Jason",
                "Barbara", "Margaret", "Betty", "Ruth", "Kimberly",
                "William", "Thomas", "Donald", "Brian", "Jeff",
                "Elizabeth", "Dorothy", "Helen", "Sharon", "Deborah"
            };

            var surnames = new[]
            {
                "Smith", "Anderson", "Clark", "Wright", "Mitchell",
                "Johnson", "Thomas", "Rodriguez", "Lopez", "Perez",
                "Williams", "Jackson", "Lewis", "Hill", "Roberts",
                "Jones", "White", "Lee", "Scott", "Turner",
                "Brown", "Harris", "Walker", "Green", "Phillips",
                "Davis", "Martin", "Hall", "Adams", "Campbell",
                "Miller", "Thompson", "Allen", "Baker", "Parker",
                "Wilson", "Garcia", "Young", "Gonzalez", "Evans",
                "Moore", "Martinez", "Hernandez", "Nelson", "Edwards",
                "Taylor", "Robinson", "King", "Carter", "Collins"
            };

            var random = new Random();

            var minDate = new DateTime(1900, 1, 1);
            TimeSpan timeSpan = DateTime.Today - minDate;

            return names.OrderBy(_ => random.Next())
                .Zip(surnames, (name, surname) => (name, surname))
                .Select(tup => new Person(tup.name, tup.surname,
                        tup.name.ToLowerInvariant() + random.Next(0, 1000) + "@gmail.com",
                        minDate + new TimeSpan(0, random.Next(0, (int) timeSpan.TotalMinutes), 0)))
                .ToList();
        }
    }
}