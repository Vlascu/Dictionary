﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tema1_Dictionar.Windows;

namespace Tema1_Dictionar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void onAdministriveClick(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            this.Close();
            authWindow.ShowDialog();
        }

        private void OnSearch(object sender, RoutedEventArgs e)
        {
            SearchWindow searchWindow = new SearchWindow();
            this.Close();
            searchWindow.ShowDialog();
        }

        private void OnGame(object sender, RoutedEventArgs e)
        {
            Game game = new Game();
            this.Close();
            game.ShowDialog();
        }
    }
}
