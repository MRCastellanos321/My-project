using UnityEngine;

// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;

namespace Generando
{

    public class Generando
    {

        public static int[,] Inicializando(int filas, int columnas)
        {

            int[,] matriz = new int[filas, columnas];
            int f = 0;
            int c = 0;

            while (f < filas)
            {
                while (c < columnas)
                {
                    matriz[f, c] = 0;
                    Console.Write(matriz[f, c]);
                    c++;
                }
                c = 0;
                f++;
            }
            return matriz;
        }
        private static int[,] GenerandoMatriz(int[,] matriz)
        {
            int columnas = matriz.Length;
            int filas = matriz.Length;
            int c = 0;
            int f = 0;
            int cambio = 0;
            int recorridas = 0;


            List<string> direcciones = new List<string> { "izquierda", "derecha", "arriba", "abajo" };
            System.Random random = new System.Random();
            int posRandom = random.Next(1, direcciones.Count);
            while (recorridas < filas * columnas)
            {
                //ciclo encargado de elegir la direccion
                while (direcciones.Count != 0)
                {
                    string direccion = direcciones[posRandom];
                    if (direccion == "derecha")
                    {
                        if (c != columnas && matriz[f, c + 1] != 1 && matriz[f, c + 1] != 2)
                        {
                            recorridas = ConvertirEnPared(matriz, f, c, recorridas);
                            c++;
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
                        if (c != 0 && matriz[f, c - 1] != 1 && matriz[f, c - 1] != 2)
                        {
                            recorridas = ConvertirEnPared(matriz, f, c, recorridas);
                            c--;
                            cambio++;
                            break;
                        }
                        else
                        {
                            direcciones.Remove("izquierda");
                            break;
                        }
                    }
                    if (direccion == "arriba")
                    {
                        if (f != filas && matriz[f + 1, c] != 1 && matriz[f + 1, c] != 2)
                        {
                            recorridas = ConvertirEnPared(matriz, f, c, recorridas);
                            f++;

                            cambio++;
                            break;
                        }
                        else
                        {
                            direcciones.Remove("arriba");
                            break;
                        }
                    }
                    if (direccion == "abajo")
                    {
                        if (f != 0 && matriz[f - 1, c] != 1 && matriz[f - 1, c] != 2)
                        {
                            recorridas = ConvertirEnPared(matriz, f, c, recorridas);
                            f--;

                            cambio++;
                            break;
                        }
                        else
                        {
                            direcciones.Remove("abajo");
                            break;
                        }
                    }
                }
                if (cambio == 1)
                {
                    cambio = 0;
                    matriz[f, c] = 1;
                    recorridas++;
                }
                else
                {
                    //funcion para cuando todas las casillas vecinas esten ocupadas
                    //AbrirCaminos(matriz);
                    

                }
               

            }
             return matriz;
        }
        //
        //

        //
        //

        public static int ConvertirEnPared(int[,] matriz, int f, int c, int recorridas)
        {
            int filas = matriz.Length;
            int columnas = matriz.Length;

            //centro
            if (f != 0 && c != 0 && f != filas - 1 && c != columnas - 1)
            {
                if (matriz[f + 1, c] == 0 && matriz[f + 1, c] != 2)
                {
                    matriz[f + 1, c] = 2;
                    recorridas++;

                }
                if (matriz[f - 1, c] == 0 && matriz[f - 1, c] != 2)
                {
                    matriz[f - 1, c] = 2;
                    recorridas++;
                }
                if (matriz[f, c + 1] == 0 && matriz[f, c + 1] != 2)
                {
                    matriz[f, c + 1] = 2;
                    recorridas++;
                }
                if (matriz[f, c - 1] == 0 && matriz[f, c - 1] != 2)
                {
                    matriz[f, c - 1] = 2;
                    recorridas++;
                }
            }

            //esquina superior izquierda
            if (f == 0 && c == 0)
            {
                if (matriz[f + 1, c] == 0 && matriz[f + 1, c] != 2)
                {
                    matriz[f + 1, c] = 2;
                    recorridas++;

                }
                if (matriz[f, c + 1] == 0 && matriz[f, c + 1] != 2)
                {
                    matriz[f, c + 1] = 2;
                    recorridas++;
                }

            }
            //fila superior
            if (f == 0 && c != 0 && c != columnas - 1)

            {
                if (matriz[f + 1, c] == 0 && matriz[f + 1, c] != 2)
                {
                    matriz[f + 1, c] = 2;
                    recorridas++;

                }
                if (matriz[f, c + 1] == 0 && matriz[f, c + 1] != 2)
                {
                    matriz[f, c + 1] = 2;
                    recorridas++;
                }

                if (matriz[f, c - 1] == 0 && matriz[f, c - 1] != 2)
                {
                    matriz[f, c - 1] = 2;
                    recorridas++;
                }
            }
            //esquina superior derecha
            if (f == 0 && c == columnas - 1)
            {
                if (matriz[f + 1, c] == 0 && matriz[f + 1, c] != 2)
                {
                    matriz[f + 1, c] = 2;
                    recorridas++;

                }

                if (matriz[f, c - 1] == 0 && matriz[f, c - 1] != 2)
                {
                    matriz[f, c - 1] = 2;
                    recorridas++;
                }

            }
            //columna derecha
            if (f != filas - 1 && f != 0 && c == columnas - 1)
            {

                if (matriz[f + 1, c] == 0 && matriz[f + 1, c] != 2)
                {
                    matriz[f + 1, c] = 2;
                    recorridas++;

                }
                if (matriz[f - 1, c] == 0 && matriz[f - 1, c] != 2)
                {
                    matriz[f - 1, c] = 2;
                    recorridas++;
                }
                if (matriz[f, c - 1] == 0 && matriz[f, c - 1] != 2)
                {
                    matriz[f, c - 1] = 2;
                    recorridas++;
                }

            }
            //esquina inferior derecha
            if (f == filas - 1 && c == columnas - 1)
            {

                if (matriz[f - 1, c] == 0 && matriz[f - 1, c] != 2)
                {
                    matriz[f - 1, c] = 2;
                    recorridas++;
                }
                if (matriz[f, c - 1] == 0 && matriz[f, c - 1] != 2)
                {
                    matriz[f, c - 1] = 2;
                    recorridas++;
                }



            }
            //fila inferior
            if (f == filas - 1 && c != columnas - 1 && c != 0)
            {

                if (matriz[f - 1, c] == 0 && matriz[f - 1, c] != 2)
                {
                    matriz[f - 1, c] = 2;
                    recorridas++;
                }
                if (matriz[f, c + 1] == 0 && matriz[f, c + 1] != 2)
                {
                    matriz[f, c + 1] = 2;
                    recorridas++;
                }
                if (matriz[f, c - 1] == 0 && matriz[f, c - 1] != 2)
                {
                    matriz[f, c - 1] = 2;
                    recorridas++;
                }

            }

            //esquina inferior izquierda

            if (f == filas - 1 && c == 0)
            {

                if (matriz[f - 1, c] == 0 && matriz[f - 1, c] != 2)
                {
                    matriz[f - 1, c] = 2;
                    recorridas++;
                }
                if (matriz[f, c + 1] == 0 && matriz[f, c + 1] != 2)
                {
                    matriz[f, c + 1] = 2;
                    recorridas++;
                }

            }
            //columna izquierda
            if (f != 0 && f != filas - 1 && c == 0)
            {
                if (matriz[f + 1, c] == 0 && matriz[f + 1, c] != 2)
                {
                    matriz[f + 1, c] = 2;
                    recorridas++;

                }
                if (matriz[f - 1, c] == 0 && matriz[f - 1, c] != 2)
                {
                    matriz[f - 1, c] = 2;
                    recorridas++;
                }
                if (matriz[f, c + 1] == 0 && matriz[f, c + 1] != 2)
                {
                    matriz[f, c + 1] = 2;
                    recorridas++;
                }
            }

            return recorridas;
        }


        public void AbrirCaminos(int[,] matriz)
        {
            int f = 0;
            int c = 0;
            while (f < matriz.Length)
            {
                while (c < matriz.Length)
                {
                    if(f==0 && c==0){
                      break;
                    }
                    
                    while(f==0 && c != 0 && c < matriz.Length - 1)
                    {
                         

                    }

                    c++;
                }
                f++;
            }

        }


        static void Main(string[] args)
        {

            Inicializando(4, 4);

        }

    }

}
/* bool enColumna = false;
           bool enColumna0 = false;
           bool enFila0 = false;
           bool enFila = false;
           while ()
           {
               int direccion = 
               int cambio = 0;
               while (cambio == 0)
               {
                   if (direccion == 1)
                   {
                       if (c < columnas && matriz[f, c++] != 1 && matriz[f, c++] != 2)
                       {
                           c++;
                           break;
                       }
                       if (c == columnas)
                       {
                           enColumna = true;

                       }

                       direccion = Random(2, 3, 4);?

                   }


                   if (direccion == 2)
                   {
                       if (c != 0 && matriz[f, c--] != 1 && matriz[f, c--] != 2)
                       {
                           c--;
                           break;
                       }
                       if (c == 0)
                       {
                           enColumna0 = true;
                       }
                       if (!enColumna)
                       {

                           direccion == Random(1, 3, 4);
                       }
                       else
                       {
                           direccion == Random(3, 4);?
                       }
                   }


                   if (direccion == 3)
                   {
                       if (f != 0 && matriz[f, c--] != 1 && matriz[f, c--] != 2)
                       {
                           f--;
                           break;
                       }
                       if (f == 0)
                       {
                           enFila0 = true;
                       }
                       if (!enColumna0 && !enColumna)
                       {
                           direccion == Random(1, 2, 4);
                       }
                       if (!enColumna && enColumna0)
                       {
                           direccion == Random(2, 4);
                       }
                       if (enColumna && !enColumna0)
                       {
                           direccion == Random(2, 4);
                       }
                       if (enColumna && enColumna0)
                       {
                           direccion == 4;
                       }

                   }
                   if (direccion == 4)
                   {
                       if (f < filas && matriz[f, c--] != 1 && matriz[f, c--] != 2)
                       {
                           f++;
                           break;
                       }

                       matriz[f, c] = 1;
                   }

               }

               //esquina superior izquierda
            if (f == 0 && c == 0)
            {
                matriz[f++, c] = 2;

                matriz[f, c++] = 2;

                matriz[f++, c++] = 2;


            }
            //fila superior
            if (f == 0 && c != 0 && c != columnas - 1)

            {
                matriz[f++, c] = 2;
                matriz[f, c++] = 2;
                matriz[f, c--] = 2;
                matriz[f++, c++] = 2;


                matriz[f++, c--] = 2;
            }
            //esquina superior derecha
            if (f == 0 && c == columnas - 1)
            {
                matriz[f++, c] = 2;


                matriz[f, c--] = 2;



                matriz[f++, c--] = 2;
            }
            //columna derecha
            if (f != filas - 1 && f != 0 && c == columnas - 1)
            {

                matriz[f++, c] = 2;
                matriz[f--, c] = 2;

                matriz[f, c--] = 2;

                matriz[f--, c--] = 2;

                matriz[f++, c--] = 2;
            }
            //esquina inferior derecha
            if (f == filas - 1 && c == columnas - 1)
            {

                matriz[f--, c] = 2;

                matriz[f, c--] = 2;

                matriz[f--, c--] = 2;


            }
            //fila inferior
            if (f == filas - 1 && c != columnas - 1 && c != 0)
            {

                matriz[f--, c] = 2;
                matriz[f, c++] = 2;
                matriz[f, c--] = 2;

                matriz[f--, c--] = 2;
                matriz[f--, c++] = 2;

            }

            //esquina inferior izquierda

            if (f == filas - 1 && c == 0)
            {

                matriz[f--, c] = 2;
                matriz[f, c++] = 2;
                matriz[f--, c++] = 2;

            }
            //columna izquierda
            if (f != 0 && f != filas - 1 && c == 0)
            {
                matriz[f++, c] = 2;
                matriz[f--, c] = 2;
                matriz[f, c++] = 2;

                matriz[f++, c++] = 2;

                matriz[f--, c++] = 2;


            }

            //centro
            if (f != 0 && c != 0 && f != filas - 1 && c != columnas - 1)
            {
                matriz[f++, c] = 2;
                matriz[f--, c] = 2;
                matriz[f, c++] = 2;
                matriz[f, c--] = 2;
                matriz[f++, c++] = 2;
                matriz[f--, c--] = 2;
                matriz[f--, c++] = 2;
                matriz[f++, c--] = 2;

            }

*/