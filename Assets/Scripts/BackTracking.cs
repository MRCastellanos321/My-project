
using System;
using System.Collections.Generic;

namespace Laberinto
{

    public class Laberinto
    {

        public static void Main(string[] args)
        {
            int[,] matriz = IniciarMatriz(51, 51);
            List<int[]> Backtrack = CrearLista();
            int f = 1;
            int c = 1;
            matriz[f, c] = 1;
            GenerandoCaminos(matriz, f, c, Backtrack);
            ImprimirDebug(matriz);
        }




        //de esta funcion sale una matriz con casillas camino(0) rodeadas de casillas pared(2) sin conexion entre los caminos
        public static int[,] IniciarMatriz(int filas, int columnas)
        // estos deben ser impares para que pueda haber una casilla centro del tablero
        {
            int[,] matriz = new int[filas, columnas];
            int f = 0;
            int c = 0;
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
            return matriz;
        }
        // esta lista va a guardar todas las casillas por las que se paso        

        private static List<int[]> CrearLista()
        {
            List<int[]> Backtrack = new List<int[]>
            {
                new int[] { 1, 1 }
            };
            return Backtrack;

        }


        private static void GenerandoCaminos(int[,] matriz, int f, int c, List<int[]> Backtrack)
        {

            List<string> direcciones = new List<string> { "izquierda", "derecha", "arriba", "abajo" };
            System.Random random = new System.Random();
            System.Random random2 = new System.Random();

            //aqui se realiza una iteracion en cada entrada a la recursividad para volver una casilla vacia un camino cada vez que exista una direcciin valida

            List<string> direccionesValidas = EsValido(matriz, f, c);

            if (direccionesValidas.Count != 0)
            {
                Backtrack.Add(new int[] { f, c }); //salva esa posicion
                Console.WriteLine("entre aqui");
                int posRandom = random.Next(0, direccionesValidas.Count);
                string direccion = direccionesValidas[posRandom];

                if (direccion == "derecha")
                {
                    Console.WriteLine(direccion);
                    c++;
                    matriz[f, c] = 1;
                    c++;
                    matriz[f, c] = 1;

                    ImprimirDebug(matriz);

                }


                else if (direccion == "izquierda")
                {
                    Console.WriteLine(direccion);
                    c--;
                    matriz[f, c] = 1;
                    c--;
                    matriz[f, c] = 1;
                    ImprimirDebug(matriz);

                }


                else if (direccion == "abajo")
                {
                    Console.WriteLine(direccion);
                    f++;
                    matriz[f, c] = 1;
                    f++;
                    matriz[f, c] = 1;
                    ImprimirDebug(matriz);
                }
                else if (direccion == "arriba")
                {
                    Console.WriteLine(direccion);
                    f--;
                    matriz[f, c] = 1;
                    f--;
                    matriz[f, c] = 1;

                    ImprimirDebug(matriz);
                }

                if (3 == random2.Next(0, 5))
                {
                    Ramificar(matriz, direcciones, f, c, direccion);
                }


                GenerandoCaminos(matriz, f, c, Backtrack);

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

                    List<string> vecinosVacios = EsValido(matriz, f, c);
                    if (f == 1 && c == 1)
                    {
                        Console.WriteLine("f y c son 1");
                        break;
                    }
                    if (vecinosVacios.Count == 0)
                    {
                        Console.WriteLine("no sirvio, cambio de s");
                        s--;
                        continue;
                    }

                    Console.WriteLine("entre a backtrack");
                    GenerandoCaminos(matriz, f, c, Backtrack);
                    break;


                }


            }


        }
        //esta funcion revisa si se puede o si se sale de los limites del array
        public static List<string> EsValido(int[,] matriz, int f, int c)
        {
            int filas = matriz.GetLength(0) - 1;
            int columnas = matriz.GetLength(1) - 1;

            List<string> direccionesValidas = new List<string>();


            if (c != columnas - 1 && matriz[f, c + 2] == 0)
            {

                Console.WriteLine("derecha valida");


                direccionesValidas.Add("derecha");

            }



            if (c != 1 && matriz[f, c - 2] == 0)
            {
                Console.WriteLine("izquierda valida");

                direccionesValidas.Add("izquierda");

            }



            if (f != filas - 1 && matriz[f + 2, c] == 0)
            {
                Console.WriteLine("abajo valida");


                direccionesValidas.Add("abajo");

            }


            if (f != 1 && matriz[f - 2, c] == 0)
            {
                Console.WriteLine("arriba valida");


                direccionesValidas.Add("arriba");

            }




            return direccionesValidas;

        }
        //funcion ramificar es para q el laberinto no quede tan recto
        private static void Ramificar(int[,] matriz, List<string> direcciones, int f, int c, string no)
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
            else if (ramificar == "arriba" && f + 1 != 0)
            {
                matriz[f + 1, c] = 1;
            }
            else if (ramificar == "abajo" && c + 1 != matriz.GetLength(0) - 1)
            {
                matriz[f - 1, c] = 1;
            }
        }

        public static void ImprimirDebug(int[,] matriz)
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