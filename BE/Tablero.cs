using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Tablero
    {
        private bool estado;

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        private bool fichanomovida;

        public bool FichaNoMovida
        {
            get { return fichanomovida; }
            set { fichanomovida = value; }
        }

        public Casillero[] casillero = new Casillero[89];

        public Casillero[] Casillero
        {
            get { return casillero; }
            set { casillero = value; }
        }

        private Jugador[] jugador = new Jugador[5];

        public Jugador[] Jugador
        {
            get { return jugador; }
            set { jugador = value; }
        }

        private int turnoactual;

        public int TurnoActual
        {
            get { return turnoactual; }
            set { turnoactual = value; }
        }

        private List<int> turnos = new List<int>();

        public List<int> Turnos
        {
            get { return turnos; }
            set { turnos = value; }
        }
    }
}
