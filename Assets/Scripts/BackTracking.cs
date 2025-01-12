
using System;
using System.Collections.Generic;


namespace Tablero
{

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
        public int GetSize()
        {
            return matriz.GetLength(0);

        }
        public void SetPosValue(int f, int c, int value)
        {
            matriz[f, c] = value;

        }

        public int Leer(int f, int c)
        {
            if (f < matriz.GetLength(0) && f > -1 && c < matriz.GetLength(1) && c > -1)
            {
                return matriz[f, c];
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
            OpenCenter();
            SpawnTraps();
            SpawnShards();
            SpawnObject(8);
            //las llaves
            SpawnObject(9);
            //skillBoost
            SpawnObject(10);
            //attackBoost
            SpawnHalfBrokenWall();
        }
        //de esta funcion sale una matriz con casillas camino(0) rodeadas de casillas pared(2) sin conexion entre los caminos
        private void IniciarMatriz()

        {
            // estos deben ser impares para que pueda haber una casilla centro del tablero y se pueda comenzar por columna piedra y terminar columna piedra
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


            Random random = new Random();
            Random random2 = new Random();

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

                if (3 == random2.Next(0, 4))
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

            Random random = new Random();
            int posRandom = random.Next(0, ramificarValidas.Count);
            string ramificar = ramificarValidas[posRandom];
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

        private void SpawnTraps()
        {
            Random random = new Random();
            int i = 0;
            //2, 4, 4
            //las trampas se quedan con una revision diferente pq ellas pueden estar en cualquier cuadrante pero no pueden estar en el centro
            while (i <= 3)
            {
                int trapF = random.Next(3, matriz.GetLength(0) - 3);
                int trapC = random.Next(3, matriz.GetLength(1) - 3);
                if (matriz[trapF, trapC] == 1 && (trapF > 27 || trapC > 27 || trapF < 22 || trapC < 22))
                {
                    matriz[trapF, trapC] = 3;
                    i++;
                }
                else
                {
                    continue;
                }
            }
            i = 0;
            while (i <= 6)
            {
                int trapF = random.Next(3, matriz.GetLength(0) - 3);
                int trapC = random.Next(3, matriz.GetLength(1) - 3);
                if (matriz[trapF, trapC] == 1 && (trapF > 27 || trapC > 27 || trapF < 22 || trapC < 22))
                {
                    matriz[trapF, trapC] = 4;
                    i++;
                }
                else
                {
                    continue;
                }
            }
            i = 0;
            while (i <= 7)
            {
                int trapF = random.Next(3, matriz.GetLength(0) - 3);
                int trapC = random.Next(3, matriz.GetLength(1) - 3);
                if (matriz[trapF, trapC] == 1 && (trapF > 27 || trapC > 27 || trapF < 22 || trapC < 22))
                {
                    matriz[trapF, trapC] = 5;
                    i++;
                }
                else
                {
                    continue;
                }
            }
        }



        private void SpawnShards()
        {
            Random random = new Random();
            int i = 0;
            //con los 4 while los shards recolectables quedan esparcidos mas equitativamente entre cuatro areas grandes
            while (i < 4)
            {   //primer cuadrante
                int shardF = random.Next(2, (matriz.GetLength(0) - 1) / 2 - 2);
                int shardC = random.Next(2, (matriz.GetLength(1) - 1) / 2 - 2);
                // comprobaciones para que no queden muy cerca de los bordes ni muy cerca del centro
                if (matriz[shardF, shardC] == 1)
                {
                    matriz[shardF, shardC] = 6;
                    i++;
                }
                else
                {
                    continue;
                }
                //&& shardF > 4 && shardC > 4 && shardF < 22 && shardC < 22
            }
            i = 0;
            while (i < 4)
            {
                int shardF = random.Next(2, (matriz.GetLength(0) - 1) / 2 - 2);
                int shardC = random.Next((matriz.GetLength(1) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                // comprobaciones para que no queden muy cerca de los bordes ni muy cerca del centro
                if (matriz[shardF, shardC] == 1)
                {
                    matriz[shardF, shardC] = 6;
                    i++;
                }
                else
                {
                    continue;
                }
                //&& shardF > 4 && shardC < 46 && shardF < 22 && shardC > 28
            }
            i = 0;
            while (i < 4)
            {
                int shardF = random.Next((matriz.GetLength(0) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                int shardC = random.Next(2, (matriz.GetLength(1) - 1) / 2 - 2);
                // comprobaciones para que no queden muy cerca de los bordes ni muy cerca del centro
                if (matriz[shardF, shardC] == 1)
                {
                    matriz[shardF, shardC] = 6;
                    i++;
                }
                else
                {
                    continue;
                }
                // && shardF < 46 && shardC > 4 && shardF > 28 && shardC < 22
            }
            i = 0;
            while (i < 4)
            {
                int shardF = random.Next((matriz.GetLength(0) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                int shardC = random.Next((matriz.GetLength(1) - 1) / 2 + 2, matriz.GetLength(1) - 2);
                // comprobaciones para que no queden muy cerca de los bordes ni muy cerca del centro
                if (matriz[shardF, shardC] == 1)
                {
                    matriz[shardF, shardC] = 6;
                    i++;
                }
                else
                {
                    continue;
                }
                //&& shardF < 46 && shardC < 46 && shardF > 28 && shardC > 28
            }
        }

        public void SpawnObject(int objectType)
        {
            Random random = new Random();
            int i = 0;
            while (i != 1)
            {   //primer cuadrante
                int f = random.Next(2, (matriz.GetLength(0) - 1) / 2 - 2);
                int c = random.Next(2, (matriz.GetLength(1) - 1) / 2 - 2);
                // comprobaciones para que no queden muy cerca de los bordes ni muy cerca del centro
                if (matriz[f, c] == 1)
                {
                    matriz[f, c] = objectType;
                    i++;
                }
                else
                {
                    continue;
                }
                //f > 4 && c > 4 && f < 22 && c < 22
            }
            i = 0;
            while (i != 1)
            {
                int f = random.Next(2, (matriz.GetLength(0) - 1) / 2 - 2);
                int c = random.Next((matriz.GetLength(1) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                // comprobaciones para que no queden muy cerca de los bordes ni muy cerca del centro
                if (matriz[f, c] == 1)
                {
                    matriz[f, c] = objectType;
                    i++;
                }
                else
                {
                    continue;
                }
                //&& f > 4 && c < 46 && f < 22 && c > 28
            }
            i = 0;
            while (i != 1)
            {
                int f = random.Next((matriz.GetLength(0) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                int c = random.Next(2, (matriz.GetLength(1) - 1) / 2 - 2);
                // comprobaciones para que no queden muy cerca de los bordes ni muy cerca del centro
                if (matriz[f, c] == 1)
                {
                    matriz[f, c] = objectType;
                    i++;
                }
                else
                {
                    continue;
                }
                //if (matriz[f, c] == 1 && f < 46 && c > 4 && f > 28 && c < 22)

            }
            i = 0;
            while (i != 1)
            {
                int f = random.Next((matriz.GetLength(0) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                int c = random.Next((matriz.GetLength(1) - 1) / 2 + 2, matriz.GetLength(1) - 2);
                // comprobaciones para que no queden muy cerca de los bordes ni muy cerca del centro
                if (matriz[f, c] == 1)
                {
                    matriz[f, c] = objectType;
                    i++;
                }
                else
                {
                    continue;
                }
                //&& f < 46 && c < 46 && f > 28 && c > 28
            }
        }
        public void SpawnHalfBrokenWall()
        {
            Random random = new Random();
            int i = 0;
            while (i != 3)
            {   //primer cuadrante
                int f = random.Next(2, (matriz.GetLength(0) - 1) / 2 - 2);
                int c = random.Next(2, (matriz.GetLength(1) - 1) / 2 - 2);
                // comprobaciones para que no queden muy cerca de los bordes ni muy cerca del centro
                if (matriz[f, c] == 2 && ((f % 2 == 0 && c % 2 != 0) || (f % 2 != 0 && c % 2 == 0)))
                {
                    matriz[f, c] = 11;
                    i++;
                }
                else
                {
                    continue;
                }
                //&& f > 3 && c > 3 && f < 22 && c < 22
            }
            i = 0;
            while (i != 3)
            {
                int f = random.Next(2, (matriz.GetLength(0) - 1) / 2 - 2);
                int c = random.Next((matriz.GetLength(1) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                // comprobaciones para que no queden muy cerca de los bordes ni muy cerca del centro
                if (matriz[f, c] == 2 && ((f % 2 == 0 && c % 2 != 0) || (f % 2 != 0 && c % 2 == 0)))
                {
                    matriz[f, c] = 11;
                    i++;
                }
                else
                {
                    continue;
                }
                // && f > 3 && c < 47 && f < 22 && c > 28
            }
            i = 0;
            while (i != 3)
            {
                int f = random.Next((matriz.GetLength(0) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                int c = random.Next(2, (matriz.GetLength(1) - 1) / 2 - 2);
                // comprobaciones para que no queden muy cerca de los bordes ni muy cerca del centro
                if (matriz[f, c] == 2 && ((f % 2 == 0 && c % 2 != 0) || (f % 2 != 0 && c % 2 == 0)))
                {
                    matriz[f, c] = 11;
                    i++;
                }
                else
                {
                    continue;
                }
                //&& f < 47 && c > 3 && f > 28 && c < 22
            }
            i = 0;
            while (i != 3)
            {
                int f = random.Next((matriz.GetLength(0) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                int c = random.Next((matriz.GetLength(1) - 1) / 2 + 2, matriz.GetLength(1) - 2);
                // comprobaciones para que no queden muy cerca de los bordes ni muy cerca del centro
                if (matriz[f, c] == 2 && ((f % 2 == 0 && c % 2 != 0) || (f % 2 != 0 && c % 2 == 0)))
                {
                    matriz[f, c] = 11;
                    i++;
                }
                else
                {
                    continue;
                }
                //&& f < 47 && c < 47 && f > 28 && c > 28
            }
        }

        public void OpenCenter()
        {
            matriz[matriz.GetLength(0) / 2, matriz.GetLength(1) / 2] = 1;
            matriz[matriz.GetLength(0) / 2 + 1, matriz.GetLength(1) / 2 + 1] = 1;
            matriz[matriz.GetLength(0) / 2 - 1, matriz.GetLength(1) / 2 - 1] = 1;
            matriz[matriz.GetLength(0) / 2 - 1, matriz.GetLength(1) / 2 + 1] = 1;
            matriz[matriz.GetLength(0) / 2 + 1, matriz.GetLength(1) / 2 - 1] = 1;
            matriz[matriz.GetLength(0) / 2 + 1, matriz.GetLength(1) / 2] = 1;
            matriz[matriz.GetLength(0) / 2 - 1, matriz.GetLength(1) / 2] = 1;
            matriz[matriz.GetLength(0) / 2, matriz.GetLength(1) / 2 + 1] = 1;
            matriz[matriz.GetLength(0) / 2, matriz.GetLength(1) / 2 - 1] = 1;
            //nueve casillas centrales

            int c = (matriz.GetLength(0) - 1) / 2 - 2;
            int f = (matriz.GetLength(0) - 1) / 2 - 2;
            while (c <= (matriz.GetLength(1) - 1) / 2 + 2)
            {
                matriz[f, c] = 2;
                c++;
            }
            c--;
            while (f <= (matriz.GetLength(1) - 1) / 2 + 2)
            {
                matriz[f, c] = 2;
                f++;
            }
            f--;
            while (c >= (matriz.GetLength(1) - 1) / 2 - 2)
            {
                matriz[f, c] = 2;
                c--;
            }
            c++;
            while (f > (matriz.GetLength(1) - 1) / 2 - 2)
            {
                matriz[f, c] = 2;
                f--;
            }
            //circulo de piedra alrededor de esas casillas del centro

            matriz[matriz.GetLength(0) / 2 + 2, matriz.GetLength(1) / 2] = 7;
            matriz[matriz.GetLength(0) / 2 - 2, matriz.GetLength(1) / 2] = 7;
            matriz[matriz.GetLength(0) / 2, matriz.GetLength(1) / 2 + 2] = 7;
            matriz[matriz.GetLength(0) / 2, matriz.GetLength(1) / 2 - 2] = 7;
            //crea 4 puertas saliendo del centro*

            c = (matriz.GetLength(0) - 1) / 2 - 3;
            f = (matriz.GetLength(0) - 1) / 2 - 3;

            while (c <= (matriz.GetLength(1) - 1) / 2 + 3)
            {
                matriz[f, c] = 1;
                c++;
            }
            c--;
            while (f <= (matriz.GetLength(1) - 1) / 2 + 3)
            {
                matriz[f, c] = 1;
                f++;
            }
            f--;
            while (c >= (matriz.GetLength(1) - 1) / 2 - 3)
            {
                matriz[f, c] = 1;
                c--;
            }
            c++;
            while (f > (matriz.GetLength(1) - 1) / 2 - 3)
            {
                matriz[f, c] = 1;
                f--;
            }
            //crea un circulo de caminos alrededor del circulo de piedra

            //todo esto es para poner cuatro puertas alrededor del centro que se tengan que desbloquear con llaves
        }

        //esto fue de cuando esta probando en consola        
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
