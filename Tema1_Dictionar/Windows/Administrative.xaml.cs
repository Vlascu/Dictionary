using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tema1_Dictionar.DataModel;

namespace Tema1_Dictionar.Windows
{
    /// <summary>
    /// Interaction logic for Administrative.xaml
    /// </summary>
    public partial class Administrative : Window
    {
        private DictionaryWord selectedWord;
        public Administrative()
        {
            InitializeComponent();
            selectedWord = null;
            List<DictionaryWord> words = JsonPersitence.LoadFromJson<DictionaryWord>(@"C:\Users\Vlascu\Desktop\Cursuri UNITBV\ANUL 2\Sem 2\MAP\Tema1_Dictionar\Tema1_Dictionar\JsonFiles\dictionary.json");
            var dataContextList = (DataContext as DictionaryWordList).DictionaryWords;

            if (words != null && words.Count > 0)
            {
                foreach (DictionaryWord word in words)
                {
                    dataContextList.Add(word);
                }
            }

        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow(this.DataContext);
            addWindow.ShowDialog();
           
        }

        private void OnRowSelected(object sender, SelectionChangedEventArgs e)
        {

            DeleteBtn.IsEnabled = true;
            EditBtn.IsEnabled = true;

            DeleteBtn.Style = (Style)FindResource("DarkGreenButtonStyle");
            EditBtn.Style = (Style)FindResource("DarkGreenButtonStyle");

            if (WordGrid.SelectedItem != null)
            {
                selectedWord = (DictionaryWord)WordGrid.SelectedItem;

            }
        }


        private void ResetGridAndButtons()
        {

            WordGrid.UnselectAll();

            EditBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;

            DeleteBtn.Style = (Style)FindResource("BlockedButtonStyle");
            EditBtn.Style = (Style)FindResource("BlockedButtonStyle");
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            var wordList = (this.DataContext as DictionaryWordList).DictionaryWords;
            var existingWord = wordList.FirstOrDefault(word => word.Name == selectedWord.Name
            && word.Description == selectedWord.Description && word.Category == selectedWord.Category);

            if (existingWord != null)
            {
                int index = wordList.IndexOf(existingWord);
                wordList.RemoveAt(index);

                List<DictionaryWord> dictionaryWords = wordList.ToList();

                JsonPersitence.SaveToJson(dictionaryWords, @"C:\Users\Vlascu\Desktop\Cursuri UNITBV\ANUL 2\Sem 2\MAP\Tema1_Dictionar\Tema1_Dictionar\JsonFiles\dictionary.json");
                MessageBox.Show("Word Deleted!");

                ResetGridAndButtons();
            }
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            ResetGridAndButtons();

            AddWindow addWindow = new AddWindow(selectedWord, this.DataContext);
            addWindow.ShowDialog();
        }
    }
}
