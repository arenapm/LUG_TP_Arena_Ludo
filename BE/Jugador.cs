using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Jugador
    {
        private int[] posficha = new int[5];

        public int[] PosFicha
        {
            get { return posficha; }
            set { posficha = value; }
        }

        private Color color;

        public Color ElColor
        {
            get { return color; }
            set { color = value; }
        }
    }
}
