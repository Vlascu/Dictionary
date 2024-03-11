using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1_Dictionar.DataModel
{
    internal class GameModel
    {
        private int roundNumber;
        private string wordName;
        private string wordDescription;
        private string image;

        public GameModel(int roundNumber, string wordName, string wordDescription, string image)
        {
            this.roundNumber = roundNumber;
            this.wordName = wordName;
            this.wordDescription = wordDescription;
            this.image = image;
        }
        public int RoundNumber { get { return roundNumber; } set { roundNumber = value; } }
        public string WordName { get { return wordName; } set { wordName = value; } }
        public string WordDescription { get { return wordDescription; } set { wordDescription = value; } }
        public string Image { get { return image; } set { image = value; } }
    }
}
