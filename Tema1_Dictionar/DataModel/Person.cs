using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema1_Dictionar.Exceptions;
using Newtonsoft.Json;

namespace Tema1_Dictionar
{
    internal class Person
    {
        private string username;
        private string password;
        public Person(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        public string Username
        {
            get { return username; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.username = value;
                } else {
                    throw new NullParameterException("You can't set a null username to a person");
                }

            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.password = value;
                } else
                {
                    throw new NullParameterException("You can't set a null password to a person");
                }
            }
        }
        
    }
}
