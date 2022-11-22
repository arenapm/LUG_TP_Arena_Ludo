using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class Tablero
    {
        DAL.Tablero mp = new DAL.Tablero();

        public BE.Tablero CrearTablero()
        {
            BE.Tablero unTablero = new BE.Tablero();

            for (int i = 1; i < 89; i++)
            {
                unTablero.Casillero[i] = new BE.Casillero();
                unTablero.Casillero[i].PicBox = new PictureBox();
            }

            for (int i = 1; i < 5; i++)
            {
                unTablero.Jugador[i] = new BE.Jugador();
            }

            unTablero.Jugador[1].ElColor = Color.Blue;
            unTablero.Jugador[2].ElColor = Color.Red;
            unTablero.Jugador[3].ElColor = Color.Green;
            unTablero.Jugador[4].ElColor = Color.Yellow;

            unTablero.FichaNoMovida = true;

            return unTablero;
        }

        public BE.Tablero ObtenerTablero(BE.Tablero unTablero)
        {
            unTablero = mp.LeerTablero(unTablero);
            return unTablero;
        }

        public void GuardarTablero(BE.Tablero unTablero)
        {
            mp.EscribirTablero(unTablero);
        }

        public int ObtenerCasillero(BE.Tablero unTablero, int jug, int ficha)
        {
            int picboxid = 0;
            int pos = unTablero.Jugador[jug].PosFicha[ficha];
            if (pos != 0)
            {
                picboxid = mp.LeerCasillero(jug, pos);
            }
            return picboxid;
        }

        public int ObtenerCasillero(int jug, int pos)
        {
            int picboxid = 0;
            if (pos != 0)
            {
                picboxid = mp.LeerCasillero(jug, pos);
            }
            return picboxid;
        }

        public int ObtenerJugador(BE.Tablero unTablero, int auxTurno)
        {
            int jug;
            if (unTablero.FichaNoMovida)
            {
                jug = unTablero.Turnos[auxTurno - 1];
            }
            else
            {
                jug = unTablero.Turnos[unTablero.TurnoActual - 1];
            }

            return jug;
        }

        public void MoverFicha(BE.Tablero unTablero, int jug, int ficha, int dado)
        {
            int pos = unTablero.Jugador[jug].PosFicha[ficha];
            int posDestino = 0;

            if (pos < 100 & dado == 6)
            {
                posDestino = 101;
            }
            
            if (pos > 100 & pos < 157)
            {
                posDestino = pos + dado;
            }

            if (posDestino > 156)
            {
                posDestino = ficha + 200;
            }

            if (pos > 200)
            {
                MessageBox.Show("Esta ficha ya cumplió el objetivo");
            }


            bool ocupada = false;
            int jugOcupado = 0;
            int fichaOcupada = 0;
            foreach (int auxJug in unTablero.Turnos)
            {
                for (int auxFicha = 1; auxFicha < 5; auxFicha++)
                {
                    int miPosicion = ObtenerCasillero(jug, posDestino);
                    int rivalPosicion = ObtenerCasillero(auxJug, unTablero.Jugador[auxJug].PosFicha[auxFicha]);
                    if (miPosicion == rivalPosicion)
                    {
                        ocupada = true;
                        jugOcupado = auxJug;
                        fichaOcupada = auxFicha;
                    }
                }
            }

            if (ocupada & jug == jugOcupado)
            {
                MessageBox.Show("El casillero está ocupado");
            }

            if (posDestino != 0 & !ocupada)
            {
                unTablero.Jugador[jug].PosFicha[ficha] = posDestino;
            }

            if (posDestino != 0 & ocupada & jug != jugOcupado)
            {
                unTablero.Jugador[jugOcupado].PosFicha[fichaOcupada] = fichaOcupada;
                unTablero.Jugador[jug].PosFicha[ficha] = posDestino;
            }
        }
    }
}
