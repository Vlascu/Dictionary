using Microsoft.Win32;
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
using System.IO;
using Tema1_Dictionar.DataModel;
using Tema1_Dictionar.Exceptions;
using System.Collections.ObjectModel;

namespace Tema1_Dictionar.Windows
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private byte[] imageBytes;
        private DictionaryWord selectedWord;
        private bool isChangingData = false;
        private ObservableCollection<string> categories;
        private string currentCategory = null;

        public AddWindow(object dContext)
        {
            InitializeComponent();
            GetCategories();
            DataContext = dContext;
            imageBytes = File.ReadAllBytes(@"C:\Users\Vlascu\Desktop\Cursuri UNITBV\ANUL 2\Sem 2\MAP\Dictionary\Tema1_Dictionar\no_image.jpg");
        }

        public AddWindow(DictionaryWord selectedWord, object dContext)
        {
            InitializeComponent();
            GetCategories();
            this.isChangingData = true;
            this.selectedWord = selectedWord;
            DataContext = dContext;
            imageBytes = File.ReadAllBytes(@"C:\Users\Vlascu\Desktop\Cursuri UNITBV\ANUL 2\Sem 2\MAP\Dictionary\Tema1_Dictionar\no_image.jpg");
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsCategoryPresent(CategoryInput.Text) && currentCategory != CategoryInput.Text)
                {
                    ErrorLabel.Content = "Category already exists, please select it from the dropdown";
                }
                else
                {

                    if(currentCategory == null)
                    {
                        currentCategory = CategoryInput.Text;
                    }

                    if (!isChangingData)
                    {

                        DictionaryWord word = new DictionaryWord()
                        {
                            Name = WordNameInput.Text,
                            Description = DescriptionInput.Text,
                            Category = currentCategory,
                            Base64Image = imageBytes
                        };
                        (DataContext as DictionaryWordList).DictionaryWords.Add(word);

                        JsonPersitence.SaveToJson(word, @"C:\Users\Vlascu\Desktop\Cursuri UNITBV\ANUL 2\Sem 2\MAP\Dictionary\Tema1_Dictionar\JsonFiles\dictionary.json");
                        MessageBox.Show("Word Added!");

                        this.Close();

                    }
                    else
                    {
                        var wordList = (DataContext as DictionaryWordList).DictionaryWords;
                        var existingWord = wordList.FirstOrDefault(word => word.Name == selectedWord.Name
                        && word.Description == selectedWord.Description && word.Category == selectedWord.Category);

                        if (existingWord != null)
                        {
                            int index = wordList.IndexOf(existingWord);
                            wordList.RemoveAt(index);

                            wordList.Insert(index, new DictionaryWord
                            {
                                Name = WordNameInput.Text,
                                Description = DescriptionInput.Text,
                                Category = currentCategory,
                                Base64Image = imageBytes
                            });

                            List<DictionaryWord> dictionaryWords = wordList.ToList();

                            JsonPersitence.SaveToJson(dictionaryWords, @"C:\Users\Vlascu\Desktop\Cursuri UNITBV\ANUL 2\Sem 2\MAP\Dictionary\Tema1_Dictionar\JsonFiles\dictionary.json");
                            MessageBox.Show("Word Updated!");

                            this.Close();
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                if (ex is NullParameterException)
                {
                    ErrorLabel.Content = ex.Message;
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                }
            }

        }

        private void OnImageAdd(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";

            string initialDirectory = System.IO.Path.GetDirectoryName(@"C:\Users\Vlascu\Desktop\Cursuri UNITBV\ANUL 2\Sem 2\MAP");

            openFileDialog.InitialDirectory = initialDirectory;

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                imageBytes = File.ReadAllBytes(imagePath);

            }
        }

        private void GetCategories()
        {
            categories = new ObservableCollection<string>();

            List<DictionaryWord> dictionaryWords = JsonPersitence.LoadFromJson<DictionaryWord>(@"C:\Users\Vlascu\Desktop\Cursuri UNITBV\ANUL 2\Sem 2\MAP\Dictionary\Tema1_Dictionar\JsonFiles\dictionary.json");

            foreach (DictionaryWord dictionaryWord in dictionaryWords)
            {
                if (!categories.Contains(dictionaryWord.Category))
                {
                    categories.Add(dictionaryWord.Category);
                }
            }

            CategoriesDropdown.ItemsSource = categories;
        }

        private bool IsCategoryPresent(string category)
        {
            foreach (string existingCategory in categories)
            {
                if (existingCategory == category) return true;
            }
            return false;
        }

        private void OnSelectedCategory(object sender, SelectionChangedEventArgs e)
        {
            if (CategoriesDropdown.SelectedItem != null)
            {
                CategoryInput.Clear();
                currentCategory = CategoriesDropdown.SelectedItem as string;
            }
        }
    }
}
