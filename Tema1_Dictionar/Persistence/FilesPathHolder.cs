using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1_Dictionar.Persistence
{
    public class FilesPathHolder
    {
        private static readonly string DICTIONARY_PATH = @"C:\Users\Vlascu\Desktop\Cursuri UNITBV\ANUL 2\Sem 2\MAP\Dictionary\Tema1_Dictionar\JsonFiles\dictionary.json";
        private static readonly string PERSONS_PATH = @"C:\Users\Vlascu\Desktop\Cursuri UNITBV\ANUL 2\Sem 2\MAP\Dictionary\Tema1_Dictionar\JsonFiles\persons.json";

        public static string GetDictionaryPath()
        {
            return DICTIONARY_PATH;
        }

        public static string GetPersonsPath()
        { return PERSONS_PATH; }
    }
}
