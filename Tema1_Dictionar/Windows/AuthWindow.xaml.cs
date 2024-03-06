using System;
using System.Collections.Generic;
using System.IO;
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
using Tema1_Dictionar.Exceptions;
using Tema1_Dictionar.Windows;

namespace Tema1_Dictionar
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }
        public void onRegister(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Text;

            try
            {
                Authenticator.Register(username, password);
                ErrorsLabel.Content = String.Empty;

                openAdminWindow();
            }
            catch (Exception ex)
            {
                if (ex is AlreadyExistingDataException || ex is NullParameterException
                    || ex is NullParameterException || ex is InvalidAuthDataException)
                {
                    ErrorsLabel.Content = ex.Message;
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                }

            }
        }

        private void onLogin(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Text;

            try
            {
                Authenticator.Login(username, password);
                ErrorsLabel.Content = String.Empty;

                openAdminWindow();
            }
            catch (Exception ex)
            {
                if (ex is AlreadyExistingDataException || ex is NullParameterException
                    || ex is NullParameterException || ex is UserNotFoundException || ex is InvalidAuthDataException)
                {
                    ErrorsLabel.Content = ex.Message;
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                }

            }
        }

        private void onGoBack(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.ShowDialog();
        }

        private void openAdminWindow()
        {
            Administrative adminWindow = new Administrative();
            this.Close();
            adminWindow.ShowDialog();
        }
    }
}
