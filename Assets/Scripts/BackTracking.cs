using UnityEngine;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using Unity.VisualScripting;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Rendering;
using NUnit.Framework;

namespace Laberinto
{

    public class Laberinto
    {


        //de esta funcion sale una matriz con casillas camino(0) rodeadas de casillas pared(2) sin conexion entre los caminos
        public static int[,] IniciarMatriz(int filas, int columnas)
        // estos deben ser impares para que pueda haber una casilla centro del tablero
        {
            int[,] matriz = new int[filas, columnas];
            int f = 1;
            int c = 1;
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


            //aqui se realiza una iteracion en cada entrada a la recursividad para volver una casilla vacia un camino cada vez que exista una direcciin valida

            List<string> direccionesValidas = EsValido(direcciones, matriz, f, c);

            if (direccionesValidas.Count != 0)
            {
                int posRandom = random.Next(1, direccionesValidas.Count);
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

                Backtrack.Add(new int[] { f, c }); //salva esa posicion

                GenerandoCaminos(matriz, f, c, Backtrack);

            }
            //entra cuando no hubo ningun cambio, es decir, casillas vecinas a la posicion actual ocupadas
            //funcion para cuando todas las casillas vecinas esten ocupadas(backtrack hasta la ultima casilla con vecinas no visitadas)
            else
            {
                int s = Backtrack.Count - 2; //el menos 2 es pq ya se comprobo a si misma y no se hizo ningun cambio
                int[] temp = Backtrack[s];

                while (s >= 0)
                {
                    temp = Backtrack[s];
                    f = temp[0];
                    c = temp[1];

                    List<string> vecinosVacios = EsValido(direcciones, matriz, f, c);
                    if (vecinosVacios.Count == 0)
                    {
                        s--;
                    }

                    else
                    {
                        GenerandoCaminos(matriz, f, c, Backtrack);
                    }

                }


            }


        }
        //esta funcion revisa si se puede o si se sale de los limites del array
        public static List<string> EsValido(List<string> direcciones, int[,] matriz, int f, int c)
        {

            int columnas = matriz.Length - 1;
            int filas = matriz.Length - 1;
            List<string> direccionesValidas = direcciones;
            for (int i = 0; i < direccionesValidas.Count; i++)
            {

                string direccion = direccionesValidas[i];
                if (direccion == "derecha")
                {
                    if (c == columnas - 1 || matriz[f, c + 2] == 1 || matriz[f, c + 2] == 2)
                    {

                        direccionesValidas.Remove("derecha");
                        break;
                    }

                }
                if (direccion == "izquierda")
                {
                    if (c == 1 || matriz[f, c - 2] == 1 || matriz[f, c - 2] == 2)
                    {
                        direccionesValidas.Remove("izquierda");
                        break;
                    }
                }
                if (direccion == "abajo")
                {
                    if (f == filas - 1 || matriz[f + 2, c] == 1 || matriz[f + 2, c] == 2)
                    {
                        direcciones.Remove("abajo");
                        break;
                    }
                }
                if (direccion == "arriba")
                {
                    if (f == 1 || matriz[f - 2, c] == 1 || matriz[f - 2, c] == 2)
                    {
                        direcciones.Remove("arriba");
                        break;
                    }
                }

            }
            return direccionesValidas;

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
                    Console.Write(matriz[f, c] + " ");

                    c++;
                }
                 Console.WriteLine();
                f++;
            }


        }

        /* List<int> Backtrack = new List<int> { 0, 0 }; // esta lista va a guardar la ultima casilla "no orillada" con vecinos vacios
         private static void GenerandoCaminos(int[,] matriz, int f, int c, List<int> Backtrack)
         {
             int columnas = matriz.Length - 1;
             int filas = matriz.Length - 1;
             int cambio = 0;


             List<string> direcciones = new List<string> { "izquierda", "derecha", "arriba", "abajo" };
             System.Random random = new System.Random();

             //Encargado de elegir la direccion(esta funcion revisa si se puede o si se sale de los limites del array)
             while (direcciones.Count != 0)
             {

                 int posRandom = random.Next(1, direcciones.Count);
                 string direccion = direcciones[posRandom];
                 if (direccion == "derecha")
                 {
                     if (c != columnas - 1 && matriz[f, c + 2] != 1 && matriz[f, c + 2] != 2)
                     {

                         c++;
                         matriz[f, c] = 1;
                         c++;
                         matriz[f, c] = 1;

                         cambio++;
                         break;
                     }

                     else
                     {
                         direcciones.Remove("derecha");
                         break;
                     }

                 }
                 if (direccion == "izquierda")
                 {
                     if (c != 1 && matriz[f, c - 2] != 1 && matriz[f, c - 2] != 2)
                     {

                         c--;
                         matriz[f, c] = 1;
                         c--;
                         matriz[f, c] = 1;
                         cambio++;
                         break;
                     }
                     else
                     {
                         direcciones.Remove("izquierda");
                         break;
                     }
                 }
                 if (direccion == "abajo")
                 {
                     if (f != filas - 1 && matriz[f + 2, c] != 1 && matriz[f + 2, c] != 2)
                     {

                         f++;
                         matriz[f, c] = 1;
                         f++;
                         matriz[f, c] = 1;

                         cambio++;
                         break;
                     }
                     else
                     {
                         direcciones.Remove("abajo");
                         break;
                     }
                 }
                 if (direccion == "arriba")
                 {
                     if (f != 1 && matriz[f - 2, c] != 1 && matriz[f - 2, c] != 2)
                     {

                         f--;
                         matriz[f, c] = 1;
                         f--;
                         matriz[f, c] = 1;

                         cambio++;
                         break;
                     }
                     else
                     {
                         direcciones.Remove("arriba");
                         break;
                     }
                 }

             }

             if (cambio == 1) //es decir, se movio la posicion y se abrio un camino
             {
                 if (f != 1 && f != filas - 1 && c != 1 && c != columnas - 1) //solo para casillas centro para abrir ramificaciones donde es mas conveniente
                 {
                     if (f + 2 == 0 || f - 2 == 0 || c - 2 == 0 || c + 2 == 0)
                     {
                         Backtrack[1] = f;
                         Backtrack[2] = c;
                     }

                     //funciona aunque sea para solo centro. todas las casillas excepto las esquinas tienen un vecino centro. si el algoritmo no ha terminado, es pq 
                     //aun quedan ceros. pero ese cero debe tener un vecino centro que en algun momento se guardo en el backtrack. por tanto cuando termine los unicos
                     //ceros que pueden quedar son las esquinas
                 }
                 GenerandoCaminos(matriz, f, c, Backtrack);


             }
             //entra cuando no hubo ningun cambio, es decir, casillas vecinas a la posicion actual ocupadas
             else
             {
                 if (Backtrack[1] != -1 && Backtrack[2] != -1)
                 {
                     //funcion para cuando todas las casillas vecinas esten ocupadas(backtrack hasta la ultima casilla centro con vecinas no visitadas)
                     f = Backtrack[1];
                     c = Backtrack[2]; //se le da la ultima posicion con vecinos en blanco
                     Backtrack[1] = -1;
                     Backtrack[2] = -1;
                     //esto es para saber que no se encontro nadie con vecinos en blanco si vuelvo a entrar y los valores son los mismos
                     GenerandoCaminos(matriz, f, c, Backtrack);
                 }

                 else
                 {
                     // se termina
                 }

             }


         }
         */
    }
}









