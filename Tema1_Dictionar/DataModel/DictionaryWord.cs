using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Tema1_Dictionar.Exceptions;

namespace Tema1_Dictionar.DataModel
{
    public class DictionaryWord : INotifyPropertyChanged
    {
        private string name;
        private string description;
        private string category;
        private string base64Image;

        public event PropertyChangedEventHandler PropertyChanged;


        public DictionaryWord(string name, string description, string category)
        {
            this.name = name;
            this.description = description;
            this.category = category;
        }
        public DictionaryWord() { }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullParameterException("You can't add a word without its name!");
                }
                this.name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public string Description
        {
            get { return this.description; }
            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullParameterException("You can't add a word without its description!");
                }
                this.description = value;
                NotifyPropertyChanged("Description");
            }
        }

        public string Category
        {
            get { return this.category; }
            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullParameterException("You can't add a word without its category!");
                }
                this.category = value;
                NotifyPropertyChanged("Category");
            }
        }

        public byte[] Base64Image
        {
            get { return Convert.FromBase64String(this.base64Image); }
            set { this.base64Image = Convert.ToBase64String(value); }
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
