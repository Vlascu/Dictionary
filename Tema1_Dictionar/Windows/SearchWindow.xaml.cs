using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        private List<string> categories;
        private string currentCategory;
        private DictionaryWord searchedWord;
        private string currentSubstring;

        public SearchWindow()
        {
            InitializeComponent();
            currentCategory = null;
            searchedWord = null;
            currentSubstring = null;

            GetAllWords();
            categories = GetCategories();
            InsertCategoriesComboBox();

            if (Resources["WordView"] is CollectionViewSource collectionViewSource)
            {
                collectionViewSource.Filter += FilterCategory;
            }
        }

        private void OnGoBack(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.ShowDialog();
        }

        private List<string> GetCategories()
        {
            List<DictionaryWord> dictionaryWords = JsonPersitence.LoadFromJson<DictionaryWord>(@"C:\Users\Vlascu\Desktop\Cursuri UNITBV\ANUL 2\Sem 2\MAP\Dictionary\Tema1_Dictionar\JsonFiles\dictionary.json");

            return dictionaryWords.Select(word => word.Category).Distinct().ToList();

        }

        private void InsertCategoriesComboBox()
        {
            foreach (string category in categories)
            {
                CategoryCombo.Items.Add(category);
            }
        }

        private void OnCategorySelected(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryCombo.SelectedItem != null)
            {
                currentCategory = (string)CategoryCombo.SelectedItem;
            }
        }

        private void OnLetterEntered(object sender, RoutedEventArgs e)
        {
            currentSubstring = WordSearch.Text;
        }

        private void GetAllWords()
        {
            var words = JsonPersitence.LoadFromJson<DictionaryWord>(@"C:\Users\Vlascu\Desktop\Cursuri UNITBV\ANUL 2\Sem 2\MAP\Dictionary\Tema1_Dictionar\JsonFiles\dictionary.json");
            foreach (var word in words)
            {
                (DataContext as DictionaryWordList).DictionaryWords.Add(word);
            }

        }
        private void FilterCategory(object sender, FilterEventArgs e)
        {
            if (e.Item is DictionaryWord word)
            {
                if (currentSubstring != null)
                {
                    e.Accepted = word.Category.Contains(currentSubstring);
                }
                if (currentCategory != null && currentSubstring != null)
                {
                    e.Accepted = word.Category == currentCategory;
                    e.Accepted = word.Category.Contains(currentSubstring);
                }
            }
        }
    }
}
