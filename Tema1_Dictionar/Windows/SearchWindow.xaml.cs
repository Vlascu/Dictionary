﻿using System;
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
using Tema1_Dictionar.Exceptions;
using Tema1_Dictionar.Persistence;

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

            if (Resources["WordsView"] is CollectionViewSource collectionViewSource)
            {
                collectionViewSource.Filter += FilterWords;
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
            List<DictionaryWord> dictionaryWords = JsonPersitence.LoadFromJson<DictionaryWord>(FilesPathHolder.GetDictionaryPath());

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
                RefreshFilter();
            }
        }

        private void OnLetterEntered(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            comboBox.IsDropDownOpen = true;

            if (comboBox != null)
            {
                TextBox textBox = comboBox.Template.FindName("PART_EditableTextBox", comboBox) as TextBox;
                if (textBox != null)
                {
                    currentSubstring = textBox.Text;
                    RefreshFilter();
                }
            }
        }

        private void GetAllWords()
        {
            var words = JsonPersitence.LoadFromJson<DictionaryWord>(FilesPathHolder.GetDictionaryPath());
            foreach (var word in words)
            {
                (DataContext as DictionaryWordList).DictionaryWords.Add(word);
            }

        }

        private void FilterWords(object sender, FilterEventArgs e)
        {
            if (e.Item is DictionaryWord word)
            {
                bool accepted = true;

                if (currentSubstring != null)
                {
                    accepted = word.Name.StartsWith(currentSubstring, StringComparison.OrdinalIgnoreCase);
                }

                if (currentCategory != null && currentCategory != "" && word.Category != currentCategory)
                {
                    accepted = false;
                }

                e.Accepted = accepted;
            }
        }

        private void RefreshFilter()
        {
            if (Resources["WordsView"] is CollectionViewSource collectionViewSource)
            {
                collectionViewSource.View.Refresh();
            }
        }

        private void OnWordSelected(object sender, SelectionChangedEventArgs e)
        {
            if (WordSearch.SelectedItem != null)
            {
                DictionaryWord word = (DictionaryWord)WordSearch.SelectedItem;
                string currentWordName = word.Name;
                searchedWord = GetSelectedWord(currentWordName);
            }
        }

        private DictionaryWord GetSelectedWord(string wordName)
        {
            return (DataContext as DictionaryWordList).DictionaryWords.FirstOrDefault(w => w.Name == wordName);
        }

        private void OnSearch(object sender, RoutedEventArgs e)
        {
            if(searchedWord != null)
            {
                WordWindow wordWindow = new WordWindow(searchedWord);
                this.Close();
                wordWindow.ShowDialog();

            } else
            {
                ErrorsLabel.Content = "Word is not selected!";
            }
        }
    }
}
