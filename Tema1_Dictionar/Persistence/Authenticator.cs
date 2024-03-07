using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tema1_Dictionar.Exceptions;

namespace Tema1_Dictionar
{
    internal class Authenticator
    {
        public static void Register(string username, string password)
        {
            InputValidation(username, password);

            List<Person> persons = JsonPersitence.LoadFromJson<Person>(@"C:\Users\Vlascu\Desktop\Cursuri UNITBV\ANUL 2\Sem 2\MAP\Dictionary\Tema1_Dictionar\JsonFiles\persons.json");

            if (persons != null)
            {
                foreach (Person person in persons)
                {
                    if (person.Username == username && person.Password == password)
                    {
                        throw new AlreadyExistingDataException("User already exists!");
                    }
                }
            }

            JsonPersitence.SaveToJson<Person>(new Person(username, password), @"C:\Users\Vlascu\Desktop\Cursuri UNITBV\ANUL 2\Sem 2\MAP\Dictionary\Tema1_Dictionar\JsonFiles\persons.json");

            MessageBox.Show("User registerd succesfully!");
        }
        public static void Login(string username, string password)
        {
            InputValidation(username,password);

            List<Person> persons = JsonPersitence.LoadFromJson<Person>(@"C:\Users\Vlascu\Desktop\Cursuri UNITBV\ANUL 2\Sem 2\MAP\Dictionary\Tema1_Dictionar\JsonFiles\persons.json");

            if (persons != null)
            {
                foreach (Person person in persons)
                {
                    if (person.Username == username && person.Password == password)
                    {
                        MessageBox.Show("User logged succesfully!");
                        return;
                    }
                }
            }
            throw new UserNotFoundException("User not found!");

        }
        private static void InputValidation(string username, string password)
        {
            if (username == null || username == string.Empty)
            {
                throw new InvalidAuthDataException("Null or empty username!");
            }
            if (password == null || password == string.Empty)
            {
                throw new InvalidAuthDataException("Null or empty password!");
            }
        }

    }
}
