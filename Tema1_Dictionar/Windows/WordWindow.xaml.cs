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
    /// Interaction logic for WordWindow.xaml
    /// </summary>
    public partial class WordWindow : Window
    {
        public WordWindow(DictionaryWord word)
        {
            InitializeComponent();

            WordName.Content = word.Name;
            WordCategory.Content = word.Category;
            WordDescription.Content = word.Description;
            WordImage.Source = GetBitmapImage(word.Base64Image);
        }
        private BitmapImage GetBitmapImage(byte[] bytes)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new System.IO.MemoryStream(bytes);
            bitmapImage.EndInit();

            return bitmapImage;
        }

        private void OnGoBack(object sender, RoutedEventArgs e)
        {
            SearchWindow searchWindow = new SearchWindow();
            this.Close();
            searchWindow.ShowDialog();

        }
    }
}
