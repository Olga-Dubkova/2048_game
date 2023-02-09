using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048WinFormsApp
{
    public class User
    {
        public string Name;
        public int Score; 
        public User (string name, int score)
        {
            Name = name; 
            Score = score;
        }
    }
}
