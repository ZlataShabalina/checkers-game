using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame
{
    public abstract class Piece
    {
        public char Icon { get; private set; }
        public bool IsKing { get; protected set; }

        public Piece(char icon)
        {
            Icon = icon;
            IsKing = false;
        }

        public void CheckPromotion(int row, int col)
        {
            if ((Icon == 'x' && row == 0) || (Icon == 'o' && row == 7))
                IsKing = true;
        }

        public override string ToString()
        {
            return IsKing ? char.ToUpper(Icon).ToString() : Icon.ToString();
        }
    }

    public class Pawn : Piece
    {
        public Pawn(char icon) : base(icon) { }
    }

    public class King : Piece
    {
        public King(char icon) : base(icon)
        {
            IsKing = true;
        }
    }
}
