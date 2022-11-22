using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LUG_TP_Arena_Ludo
{
    public partial class Form1 : Form
    {
        BE.Tablero mitablero = new BE.Tablero();
        BLL.Tablero gestorTablero = new BLL.Tablero();
        Random dado = new Random();
        int auxTurno;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mitablero = gestorTablero.CrearTablero();
            mitablero = gestorTablero.ObtenerTablero(mitablero);
            AsignarPicBoxes();
            ActualizarTablero();
            ActualizarControles();

            if (mitablero.Estado)
            {
                auxTurno = mitablero.TurnoActual;
                btIniciar.Enabled = false;
                labelTurno.Text = Enum.GetName(typeof(BE.ColorJugador), mitablero.Turnos[mitablero.TurnoActual - 1]);
                btDado.Enabled = true;
            }
        }

        private void btIniciar_Click(object sender, EventArgs e)
        {
            if (listJugadores.CheckedItems.Count < 2)
            {
                MessageBox.Show("Debe seleccionar al menos dos jugadores para iniciar una partida");
            }
            else
            {
                mitablero.Estado = true;
                mitablero.TurnoActual = 1;
                int casilla;
                int jug;
                foreach (int i in listJugadores.CheckedIndices)
                {
                    jug = i + 1;
                    mitablero.Turnos.Add(jug);

                    for (int ficha = 1; ficha < 5; ficha++)
                    {
                        mitablero.Jugador[jug].PosFicha[ficha] = ficha;
                        casilla = gestorTablero.ObtenerCasillero(mitablero, jug, ficha);
                        DibujarFicha(casilla, jug, ficha);
                    }
                }

                btIniciar.Enabled = false;
                labelTurno.Text = Enum.GetName(typeof(BE.ColorJugador), mitablero.Turnos[mitablero.TurnoActual - 1]);
                btDado.Enabled = true;
                listJugadores.Enabled = false;
                btReiniciar.Enabled = true;
            }

            gestorTablero.GuardarTablero(mitablero);
            ActualizarTablero();
            ActualizarControles();
        }

        private void btDado_Click(object sender, EventArgs e)
        {
            auxTurno = mitablero.TurnoActual;
            if (mitablero.TurnoActual + 1 > mitablero.Turnos.Count())
            {
                mitablero.TurnoActual = 1;
                mitablero.FichaNoMovida = true;
            }
            else
            {
                mitablero.TurnoActual++;
                mitablero.FichaNoMovida = true;
            }
            gestorTablero.GuardarTablero(mitablero);
            labelDado.Text = dado.Next(1, 7).ToString();
            HabilitarBotonesFicha();
            btDado.Enabled = false;
            btSiguiente.Enabled = true;
        }

        private void btFicha1_Click(object sender, EventArgs e)
        {
            int ficha = 1;
            int jug = gestorTablero.ObtenerJugador(mitablero, auxTurno);
            int dado = int.Parse(labelDado.Text);
            gestorTablero.MoverFicha(mitablero, jug, ficha, dado);
            gestorTablero.GuardarTablero(mitablero);
            Siguiente();
        }

        private void btFicha2_Click(object sender, EventArgs e)
        {
            int ficha = 2;
            int jug = gestorTablero.ObtenerJugador(mitablero, auxTurno);
            int dado = int.Parse(labelDado.Text);
            gestorTablero.MoverFicha(mitablero, jug, ficha, dado);
            gestorTablero.GuardarTablero(mitablero);
            Siguiente();
        }

        private void btFicha3_Click(object sender, EventArgs e)
        {
            int ficha = 3;
            int jug = gestorTablero.ObtenerJugador(mitablero, auxTurno);
            int dado = int.Parse(labelDado.Text);
            gestorTablero.MoverFicha(mitablero, jug, ficha, dado);
            gestorTablero.GuardarTablero(mitablero);
            Siguiente();
        }

        private void btFicha4_Click(object sender, EventArgs e)
        {
            int ficha = 4;
            int jug = gestorTablero.ObtenerJugador(mitablero, auxTurno);
            int dado = int.Parse(labelDado.Text);
            gestorTablero.MoverFicha(mitablero, jug, ficha, dado);
            gestorTablero.GuardarTablero(mitablero);
            Siguiente();
        }

        private void btSiguiente_Click(object sender, EventArgs e)
        {
            Siguiente();
        }

        private void Siguiente()
        {
            btDado.Enabled = true;
            labelDado.Text = "#";
            DesabilitarBotonesFicha();

            ActualizarTablero();
            ActualizarControles();
            VerificarGanador();
        }

        private void btReiniciar_Click(object sender, EventArgs e)
        {
            Reiniciar();
        }

        private void Reiniciar()
        {
            mitablero.Estado = false;
            mitablero.TurnoActual = 0;
            mitablero.Turnos.Clear();

            for (int jug = 1; jug < 5; jug++)
            {
                mitablero.Jugador[jug].PosFicha[1] = 0;
                mitablero.Jugador[jug].PosFicha[2] = 0;
                mitablero.Jugador[jug].PosFicha[3] = 0;
                mitablero.Jugador[jug].PosFicha[4] = 0;
            }

            listJugadores.Enabled = true;
            for (int i = 0; i < listJugadores.Items.Count; i++)
            {
                listJugadores.SetItemChecked(i, false);
            }
            listJugadores.ClearSelected();

            btIniciar.Enabled = true;
            labelTurno.Text = "...";
            labelTurno.BackColor = Color.Transparent;
            btDado.Enabled = false;
            labelDado.Text = "#";
            DesabilitarBotonesFicha();
            btSiguiente.Enabled = false;
            btReiniciar.Enabled = false;

            gestorTablero.GuardarTablero(mitablero);
            ActualizarTablero();
            ActualizarControles();
        }

        public void AsignarPicBoxes()
        {
            for (int i = 1; i < 89; i++)
            {
                string pbox = String.Format("pictureBox{0}", i);
                mitablero.Casillero[i].PicBox = (PictureBox)Controls.Find(pbox, true).FirstOrDefault();
            }
        }

        public void ActualizarTablero()
        {
            for (int casilla = 1; casilla < 89; casilla++)
            {
                mitablero.Casillero[casilla].PicBox.BackgroundImage = null;
            }

            int picboxid;
            for (int jug = 1; jug < 5; jug++)
            {
                for (int ficha = 1; ficha < 5; ficha++)
                {
                    picboxid = gestorTablero.ObtenerCasillero(mitablero, jug, ficha);
                    if (picboxid != 0)
                    {
                        DibujarFicha(picboxid, jug, ficha);
                    }
                }
            }
        }

        public void ActualizarControles()
        {
            if (mitablero.Estado)
            {
                foreach (int index1 in mitablero.Turnos)
                {
                    listJugadores.SetItemChecked(index1 - 1, true);
                }
                listJugadores.Enabled = false;

                int index2 = mitablero.TurnoActual;
                int jug = mitablero.Turnos[index2 - 1];
                labelTurno.Text = Enum.GetName(typeof(BE.ColorJugador), jug);
                labelTurno.BackColor = mitablero.Jugador[jug].ElColor;
            }
        }

        public void DibujarFicha(int casilla, int jug, int ficha)
        {
            string img = String.Format("ficha{0}{1}", jug, ficha);
            mitablero.Casillero[casilla].PicBox.BackgroundImage =
                (Bitmap)Properties.Resources.ResourceManager.GetObject(img);
        }

        public void HabilitarBotonesFicha()
        {
            btFicha1.Enabled = true;
            btFicha2.Enabled = true;
            btFicha3.Enabled = true;
            btFicha4.Enabled = true;
        }

        public void DesabilitarBotonesFicha()
        {
            btFicha1.Enabled = false;
            btFicha2.Enabled = false;
            btFicha3.Enabled = false;
            btFicha4.Enabled = false;
        }

        public void VerificarGanador()
        {
            bool ganador = true;
            int turno = mitablero.TurnoActual;
            if (turno == 1)
            {
                turno = mitablero.Turnos.Count();
            }
            else
            {
                turno--;
            }
            int jug = mitablero.Turnos[turno - 1];

            for (int ficha = 1; ficha < 5; ficha++)
            {
                if (mitablero.Jugador[jug].PosFicha[ficha] < 200)
                {
                    ganador = false;
                }
            }

            if (ganador)
            {
                string nom = Enum.GetName(typeof(BE.ColorJugador), jug);
                string mensaje = String.Format("EL JUGADOR {0} ES EL CAMPEÓN DEL LUDO", nom);
                MessageBox.Show(mensaje);
                Reiniciar();
            }
        }
        
    }
}
