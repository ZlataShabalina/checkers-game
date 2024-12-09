using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame
{
    public class Player
    {
        public string Name { get; private set; }
        public char Icon { get; private set; }

        public Player(string name, char icon)
        {
            Name = name;
            Icon = icon;
        }
    }
}
