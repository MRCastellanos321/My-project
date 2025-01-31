
using System;
using System.Collections.Generic;

namespace Tablero
{
    public class Laberinto
    {
        private int[,] matriz;
        private readonly List<string> directions;
        private int f;
        private int c;

        public static Laberinto ElLaberinto;

        public Laberinto(int lado) : this(lado, lado) { }
        //constructor
        private Laberinto(int filas, int columnas)
        {
            directions = new List<string> { "izquierda", "derecha", "arriba", "abajo" };
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

        public int Read(int f, int c)
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
            PathGeneration(Backtrack);
            OpenCenter();
            SpawnTraps();
            SpawnObject(8);
            //las llaves
            SpawnObject(9);
            //skillBoost
            SpawnObject(10);
            //attackBoost
            SpawnHalfBrokenWall();
            //numero 11
            SpawnShards();
            //numero 12
            matriz[(matriz.GetLength(0) - 1) / 2, (matriz.GetLength(1) - 1) / 2] = 14;
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

        private void PathGeneration(List<int[]> Backtrack)
        {
            Random random = new Random();
            Random random2 = new Random();

            //aquí se realiza una iteración en cada entrada a la recursividad para volver una casilla vacía un camino cada vez que exista una dirección válida

            List<string> validDirections = IsValid(f, c);

            if (validDirections.Count != 0)
            {
                Backtrack.Add(new int[] { f, c }); //salva esa posición
                Console.WriteLine("entre aqui");
                int posRandom = random.Next(0, validDirections.Count);
                string direction = validDirections[posRandom];

                if (direction == "derecha")
                {
                    c++;
                    matriz[f, c] = 1;
                    c++;
                    matriz[f, c] = 1;
                }
                else if (direction == "izquierda")
                {
                    c--;
                    matriz[f, c] = 1;
                    c--;
                    matriz[f, c] = 1;
                }
                else if (direction == "abajo")
                {
                    f++;
                    matriz[f, c] = 1;
                    f++;
                    matriz[f, c] = 1;
                }
                else if (direction == "arriba")
                {
                    f--;
                    matriz[f, c] = 1;
                    f--;
                    matriz[f, c] = 1;
                }

                if (3 == random2.Next(0, 4))
                {
                    Branch(direction);
                }
                PathGeneration(Backtrack);
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

                    List<string> emptyNeighbors = IsValid(f, c);
                    if (f == 1 && c == 1)
                    {
                        break;
                    }
                    if (emptyNeighbors.Count == 0)
                    {
                        s--;
                        continue;
                    }
                    PathGeneration(Backtrack);
                    break;
                }
            }
        }
        //esta funcion revisa si se puede o si se sale de los limites del array
        public List<string> IsValid(int f, int c)
        {
            int filas = matriz.GetLength(0) - 1;
            int columnas = matriz.GetLength(1) - 1;

            List<string> validDirections = new List<string>();


            if (c != columnas - 1 && matriz[f, c + 2] == 0)
            {
                validDirections.Add("derecha");
            }


            if (c != 1 && matriz[f, c - 2] == 0)
            {
                validDirections.Add("izquierda");
            }


            if (f != filas - 1 && matriz[f + 2, c] == 0)
            {
                validDirections.Add("abajo");
            }


            if (f != 1 && matriz[f - 2, c] == 0)
            {
                validDirections.Add("arriba");
            }

            return validDirections;

        }
        //funcion branch es para q el laberinto no quede tan recto
        private void Branch(string no)
        {
            List<string> validBranches = new List<string>();
            validBranches.AddRange(directions);
            validBranches.Remove(no);

            Random random = new Random();
            int posRandom = random.Next(0, validBranches.Count);
            string branch = validBranches[posRandom];
            if (branch == "izquierda" && c - 1 != 0)
            {
                matriz[f, c - 1] = 1;
            }
            else if (branch == "derecha" && c + 1 != matriz.GetLength(1) - 1)
            {
                matriz[f, c + 1] = 1;
            }
            else if (branch == "arriba" && f - 1 != 0)
            {
                matriz[f - 1, c] = 1;
            }
            else if (branch == "abajo" && f + 1 != matriz.GetLength(0) - 1)
            {
                matriz[f + 1, c] = 1;
            }
        }

        private void SpawnTraps()
        {
            Random random = new Random();
            int i = 0;
            //las trampas se quedan con una revision diferente pq ellas pueden estar en cualquier cuadrante pero no pueden estar en el centro
            while (i <= 7)
            {
                int trapF = random.Next(3, matriz.GetLength(0) - 3);
                int trapC = random.Next(3, matriz.GetLength(1) - 3);
                if (matriz[trapF, trapC] == 1 && (trapF > 27 || trapC > 27 || trapF < 22 || trapC < 22))
                {
                    matriz[trapF, trapC] = 3;
                    i++;
                }
                //Esta es la trampa que disminuye tus shards. En el juego hay 16 shards. En caso de que todas fueran activadas e hicieran efecto, 
                //quedarían 9 shards en juego. Son 4 jugadores, si los shards fueran distribuidos de 2 en 2, sobraría 1 que iría a uno de los jugadores
                //Entonces no puede ocurrir el caso de que nadie se convierta en humano porque siempre hay suficientes shards para al menos 1.
            }
            i = 0;
            while (i <= 10)
            {
                int trapF = random.Next(3, matriz.GetLength(0) - 3);
                int trapC = random.Next(3, matriz.GetLength(1) - 3);
                if (matriz[trapF, trapC] == 1 && (trapF > 27 || trapC > 27 || trapF < 22 || trapC < 22))
                {
                    matriz[trapF, trapC] = 4;//perder turnos
                    i++;
                }
            }
            i = 0;
            while (i <= 10)
            {
                int trapF = random.Next(3, matriz.GetLength(0) - 3);
                int trapC = random.Next(3, matriz.GetLength(1) - 3);
                if (matriz[trapF, trapC] == 1 && (trapF > 27 || trapC > 27 || trapF < 22 || trapC < 22))
                {
                    matriz[trapF, trapC] = 5;//enfría el ataque
                    i++;
                }
            }
            i = 0;
            while (i <= 10)
            {
                int trapF = random.Next(3, matriz.GetLength(0) - 3);
                int trapC = random.Next(3, matriz.GetLength(1) - 3);
                if (matriz[trapF, trapC] == 1 && (trapF > 27 || trapC > 27 || trapF < 22 || trapC < 22))
                {
                    matriz[trapF, trapC] = 6;//no poder ver el mapa
                    i++;
                }
            }
            i = 0;
            while (i <= 10)
            {
                int trapF = random.Next(3, matriz.GetLength(0) - 3);
                int trapC = random.Next(3, matriz.GetLength(1) - 3);
                if (matriz[trapF, trapC] == 1 && (trapF > 27 || trapC > 27 || trapF < 22 || trapC < 22))
                {
                    matriz[trapF, trapC] = 7;//reduce tirada de dados
                    i++;
                }
            }
        }

        private void SpawnShards()
        {
            Random random = new Random();
            int i = 0;
            //con los 4 while los shards recolectables quedan esparcidos mas equitativamente entre cuatro areas grandes
            while (i < 4)
            {
                int shardF = random.Next(2, (matriz.GetLength(0) - 1) / 2 - 2);
                int shardC = random.Next(2, (matriz.GetLength(1) - 1) / 2 - 2);
                if (matriz[shardF, shardC] == 1)
                {
                    matriz[shardF, shardC] = 12;
                    i++;
                }
            }
            i = 0;
            while (i < 4)
            {
                int shardF = random.Next(2, (matriz.GetLength(0) - 1) / 2 - 2);
                int shardC = random.Next((matriz.GetLength(1) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                if (matriz[shardF, shardC] == 1)
                {
                    matriz[shardF, shardC] = 12;
                    i++;
                }
            }
            i = 0;
            while (i < 4)
            {
                int shardF = random.Next((matriz.GetLength(0) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                int shardC = random.Next(2, (matriz.GetLength(1) - 1) / 2 - 2);
                if (matriz[shardF, shardC] == 1)
                {
                    matriz[shardF, shardC] = 12;
                    i++;
                }
            }
            i = 0;
            while (i < 4)
            {
                int shardF = random.Next((matriz.GetLength(0) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                int shardC = random.Next((matriz.GetLength(1) - 1) / 2 + 2, matriz.GetLength(1) - 2);
                if (matriz[shardF, shardC] == 1)
                {
                    matriz[shardF, shardC] = 12;
                    i++;
                }
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

                if (matriz[f, c] == 1)
                {
                    matriz[f, c] = objectType;
                    i++;
                }
            }
            i = 0;
            while (i != 1)
            {
                int f = random.Next(2, (matriz.GetLength(0) - 1) / 2 - 2);
                int c = random.Next((matriz.GetLength(1) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                if (matriz[f, c] == 1)
                {
                    matriz[f, c] = objectType;
                    i++;
                }
            }
            i = 0;
            while (i != 1)
            {
                int f = random.Next((matriz.GetLength(0) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                int c = random.Next(2, (matriz.GetLength(1) - 1) / 2 - 2);

                if (matriz[f, c] == 1)
                {
                    matriz[f, c] = objectType;
                    i++;
                }
            }
            i = 0;
            while (i != 1)
            {
                int f = random.Next((matriz.GetLength(0) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                int c = random.Next((matriz.GetLength(1) - 1) / 2 + 2, matriz.GetLength(1) - 2);

                if (matriz[f, c] == 1)
                {
                    matriz[f, c] = objectType;
                    i++;
                }
            }
        }
        public void SpawnHalfBrokenWall()
        {
            Random random = new Random();
            int i = 0;
            while (i != 2)
            {
                int f = random.Next(2, (matriz.GetLength(0) - 1) / 2 - 2);
                int c = random.Next(2, (matriz.GetLength(1) - 1) / 2 - 2);

                if (matriz[f, c] == 2 && ((f % 2 == 0 && c % 2 != 0) || (f % 2 != 0 && c % 2 == 0)))
                {
                    matriz[f, c] = 11;
                    i++;
                }
            }
            i = 0;
            while (i != 2)
            {
                int f = random.Next(2, (matriz.GetLength(0) - 1) / 2 - 2);
                int c = random.Next((matriz.GetLength(1) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                // la comprobación de la paridad esta relacionada a la forma de iniciar la matriz, así se optimiza la utiidad de la ubicación de la pared rompibk=le
                if (matriz[f, c] == 2 && ((f % 2 == 0 && c % 2 != 0) || (f % 2 != 0 && c % 2 == 0)))
                {
                    matriz[f, c] = 11;
                    i++;
                }
            }
            i = 0;
            while (i != 2)
            {
                int f = random.Next((matriz.GetLength(0) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                int c = random.Next(2, (matriz.GetLength(1) - 1) / 2 - 2);

                if (matriz[f, c] == 2 && ((f % 2 == 0 && c % 2 != 0) || (f % 2 != 0 && c % 2 == 0)))
                {
                    matriz[f, c] = 11;
                    i++;
                }
            }
            i = 0;
            while (i != 2)
            {
                int f = random.Next((matriz.GetLength(0) - 1) / 2 + 2, matriz.GetLength(0) - 2);
                int c = random.Next((matriz.GetLength(1) - 1) / 2 + 2, matriz.GetLength(1) - 2);

                if (matriz[f, c] == 2 && ((f % 2 == 0 && c % 2 != 0) || (f % 2 != 0 && c % 2 == 0)))
                {
                    matriz[f, c] = 11;
                    i++;
                }
            }
            //lo siguiente es para crear paredes rompibles una vez aparezcan demasiadas paredes seguidas
            int n = 1;
            int m = 1;
            int count = 0;
            while (m < matriz.GetLength(1) - 1)
            {
                while (n < matriz.GetLength(0) - 1)
                {
                    if (matriz[n, m] == 2)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }
                    if (count == 11)
                    {
                        matriz[n - 3, m] = 11;
                        count = 0;

                    }
                    n++;
                }
                n = 1;
                m++;
            }
            n = 1;
            m = 1;
            count = 0;
            while (n < matriz.GetLength(0) - 1)
            {
                while (m < matriz.GetLength(1) - 1)
                {
                    if (matriz[n, m] == 2)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }
                    if (count == 11)
                    {
                        matriz[n, m - 3] = 11;
                        count = 0;

                    }
                    m++;
                }
                m = 1;
                n++;
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

            matriz[matriz.GetLength(0) / 2 + 2, matriz.GetLength(1) / 2] = 13;
            matriz[matriz.GetLength(0) / 2 - 2, matriz.GetLength(1) / 2] = 13;
            matriz[matriz.GetLength(0) / 2, matriz.GetLength(1) / 2 + 2] = 13;
            matriz[matriz.GetLength(0) / 2, matriz.GetLength(1) / 2 - 2] = 13;
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
    }
}
