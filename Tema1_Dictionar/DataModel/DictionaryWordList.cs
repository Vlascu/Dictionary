using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1_Dictionar.DataModel
{
    class DictionaryWordList
    {
        public ObservableCollection<DictionaryWord> DictionaryWords { get; set; }
        public DictionaryWordList() { this.DictionaryWords = new ObservableCollection<DictionaryWord>(); }
    }
}
