using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Tablero
    {
        Acceso acceso = new Acceso();

        public BE.Tablero LeerTablero(BE.Tablero unTablero)
        {
            acceso.Abrir();
            DataTable tablero = acceso.Leer("SELECT * FROM TABLERO");
            DataTable turnos = acceso.Leer("SELECT * FROM TURNO");
            DataTable jugadores = acceso.Leer("SELECT * FROM JUGADOR");
            acceso.Cerrar();

            foreach (DataRow registro in tablero.Rows)
            {
                if (registro["ESTADO"].ToString() == "1")
                {
                    unTablero.Estado = true;
                }
                else
                {
                    unTablero.Estado = false;
                }
                unTablero.TurnoActual = int.Parse(registro["TURNO_ACTUAL"].ToString());
            }

            int i = 1;

            List<int> losturnos = new List<int>();
            foreach (DataRow registro in turnos.Rows)
            {
                losturnos.Add(int.Parse(registro["JUGADOR"].ToString()));
            }
            unTablero.Turnos = losturnos;

            i = 1;
            foreach (DataRow registro in jugadores.Rows)
            {
                unTablero.Jugador[i].PosFicha[1] = int.Parse(registro["POS_FICHA1"].ToString());
                unTablero.Jugador[i].PosFicha[2] = int.Parse(registro["POS_FICHA2"].ToString());
                unTablero.Jugador[i].PosFicha[3] = int.Parse(registro["POS_FICHA3"].ToString());
                unTablero.Jugador[i].PosFicha[4] = int.Parse(registro["POS_FICHA4"].ToString());
                i++;
            }

            return unTablero;
        }

        public void EscribirTablero(BE.Tablero unTablero)
        {
            string sql;
            acceso.Abrir();

            int estado = 0;
            if (unTablero.Estado) estado = 1;
            sql = String.Format("UPDATE TABLERO SET ESTADO = {0}, TURNO_ACTUAL = {1} WHERE ID = 1", estado, unTablero.TurnoActual);
            acceso.Escribir(sql);

            if (unTablero.Estado)
            {
                int fin = unTablero.Turnos.Count() + 1;
                for (int i = 1; i < fin; i++)
                {
                    sql = String.Format("INSERT INTO TURNO (ID,JUGADOR) VALUES ({0},{1})", i, unTablero.Turnos[i-1]);
                    acceso.Escribir(sql);
                }
            }
            else
            {
                sql = String.Format("DELETE FROM TURNO");
                acceso.Escribir(sql);
            }

            for (int i = 1; i < 5; i++)
            {
                sql = String.Format(
                    "UPDATE JUGADOR SET POS_FICHA1 = {0}, POS_FICHA2 = {1}, POS_FICHA3 = {2}, POS_FICHA4 = {3} WHERE ID = {4}",
                    unTablero.Jugador[i].PosFicha[1],
                    unTablero.Jugador[i].PosFicha[2],
                    unTablero.Jugador[i].PosFicha[3],
                    unTablero.Jugador[i].PosFicha[4],
                    i);
                acceso.Escribir(sql);
            }

            acceso.Cerrar();
        }

        public int LeerCasillero(int jug, int pos)
        {
            int picboxid;
            acceso.Abrir();
            string sql = String.Format("SELECT JUG_{0} FROM REFERENCIA WHERE POS = {1}", jug, pos);
            picboxid = acceso.LeerEscalar(sql);
            acceso.Cerrar();
            return picboxid;
        }

    }
}
