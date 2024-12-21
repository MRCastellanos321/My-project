
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.Properties;
using UnityEngine.Rendering.Universal.Internal;

namespace Tablero
{
    public class Program
    {
        /* public static void Main(string[] args)
         {
             var laberinto = new Laberinto(51);
             laberinto.Iniciar();
             laberinto.ImprimirDebug();
         }
 */
    }
    public class Laberinto
    {
        private int[,] matriz;
        private readonly List<string> direcciones;
        private int f;
        private int c;

        public static Laberinto ElLaberinto;

        public Laberinto(int lado) : this(lado, lado) { }
        //constructor
        private Laberinto(int filas, int columnas)
        {
            direcciones = new List<string> { "izquierda", "derecha", "arriba", "abajo" };
            matriz = new int[filas, columnas];

        }


        public int Lado()
        {
            return matriz.GetLength(0);

        }

        public int Leer(int x, int y)
        {
            if (x <= matriz.GetLength(0) && x >= -1 && y <= matriz.GetLength(1) && y >= -1)
            {
                return matriz[x, y];
            }

            else
            {
                return 2;
            }
        }

        public void Iniciar()
        {
            IniciarMatriz();
            List<int[]> Backtrack = CrearLista();
            GenerandoCaminos(Backtrack);

        }
        //de esta funcion sale una matriz con casillas camino(0) rodeadas de casillas pared(2) sin conexion entre los caminos
        private void IniciarMatriz()
        // estos deben ser impares para que pueda haber una casilla centro del tablero
        {
            int filas = matriz.GetLength(0);
            int columnas = matriz.GetLength(1);


            while (f < filas)
            {
                while (c < columnas)
                {
                    if (f % 2 != 0)
                    {
                        if (c % 2 != 0)
                        {
                            matriz[f, c] = 0;
                            //el cero es para el camino sin visitar
                        }
                        else
                        {
                            matriz[f, c] = 2;
                            //el 2 es para las paredes
                        }

                    }
                    else
                    {
                        matriz[f, c] = 2;
                    }
                    c++;
                }
                f++;
                c = 0;
            }
            f = 1;
            c = 1;
            matriz[f, c] = 1;

        }
        // esta lista va a guardar todas las casillas por las que se paso        

        public static List<int[]> CrearLista()
        {
            List<int[]> Backtrack = new List<int[]>
            {
                new int[] { 1, 1 }
            };
            return Backtrack;

        }

        private void GenerandoCaminos(List<int[]> Backtrack)
        {


            System.Random random = new System.Random();
            System.Random random2 = new System.Random();

            //aqui se realiza una iteracion en cada entrada a la recursividad para volver una casilla vacia un camino cada vez que exista una direcciin valida

            List<string> direccionesValidas = EsValido(f, c);

            if (direccionesValidas.Count != 0)
            {
                Backtrack.Add(new int[] { f, c }); //salva esa posicion
                Console.WriteLine("entre aqui");
                int posRandom = random.Next(0, direccionesValidas.Count);
                string direccion = direccionesValidas[posRandom];

                if (direccion == "derecha")
                {
                    c++;
                    matriz[f, c] = 1;
                    c++;
                    matriz[f, c] = 1;
                }


                else if (direccion == "izquierda")
                {
                    c--;
                    matriz[f, c] = 1;
                    c--;
                    matriz[f, c] = 1;
                }


                else if (direccion == "abajo")
                {
                    f++;
                    matriz[f, c] = 1;
                    f++;
                    matriz[f, c] = 1;
                }
                else if (direccion == "arriba")
                {
                    f--;
                    matriz[f, c] = 1;
                    f--;
                    matriz[f, c] = 1;
                }

                if (3 == random2.Next(0, 5))
                {
                    Ramificar(direccion);
                }


                GenerandoCaminos(Backtrack);

            }
            //entra cuando no hubo ningun cambio, es decir, casillas vecinas a la posicion actual ocupadas
            //funcion para cuando todas las casillas vecinas esten ocupadas(backtrack hasta la ultima casilla con vecinas no visitadas)
            else
            {
                int s = Backtrack.Count - 1;
                int[] temp = Backtrack[s];

                while (s >= 0)
                {
                    temp = Backtrack[s];
                    f = temp[0];
                    c = temp[1];

                    List<string> vecinosVacios = EsValido(f, c);
                    if (f == 1 && c == 1)
                    {
                        break;
                    }
                    if (vecinosVacios.Count == 0)
                    {
                        s--;
                        continue;
                    }
                    GenerandoCaminos(Backtrack);
                    break;

                }
            }
        }
        //esta funcion revisa si se puede o si se sale de los limites del array
        public List<string> EsValido(int f, int c)
        {
            int filas = matriz.GetLength(0) - 1;
            int columnas = matriz.GetLength(1) - 1;

            List<string> direccionesValidas = new List<string>();


            if (c != columnas - 1 && matriz[f, c + 2] == 0)
            {
                direccionesValidas.Add("derecha");
            }


            if (c != 1 && matriz[f, c - 2] == 0)
            {
                direccionesValidas.Add("izquierda");
            }


            if (f != filas - 1 && matriz[f + 2, c] == 0)
            {
                direccionesValidas.Add("abajo");
            }


            if (f != 1 && matriz[f - 2, c] == 0)
            {
                direccionesValidas.Add("arriba");
            }

            return direccionesValidas;

        }
        //funcion ramificar es para q el laberinto no quede tan recto
        private void Ramificar(string no)
        {
            List<string> ramificarValidas = new List<string>();
            ramificarValidas.AddRange(direcciones);
            ramificarValidas.Remove(no);

            System.Random random2 = new Random();
            int posRandom2 = random2.Next(0, ramificarValidas.Count);
            string ramificar = ramificarValidas[posRandom2];
            if (ramificar == "izquierda" && c - 1 != 0)
            {
                matriz[f, c - 1] = 1;
            }
            else if (ramificar == "derecha" && c + 1 != matriz.GetLength(1) - 1)
            {
                matriz[f, c + 1] = 1;
            }
            else if (ramificar == "arriba" && f - 1 != 0)
            {
                matriz[f - 1, c] = 1;
            }
            else if (ramificar == "abajo" && f + 1 != matriz.GetLength(0) - 1)
            {
                matriz[f + 1, c] = 1;
            }
        }

        public void ImprimirDebug()
        {
            int filas = matriz.GetLength(0);
            int columnas = matriz.GetLength(1);
            int f = 0;
            int c = 0;
            while (f < filas)
            {
                while (c < columnas)
                {

                    Console.Write(matriz[f, c] == 1 ? "  " : 22);

                    c++;
                }
                c = 0;
                f++;
                Console.WriteLine();
            }
        }
    }
}