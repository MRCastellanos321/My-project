using UnityEngine;
namespace Tablero
{
    public class SpawnMaze : MonoBehaviour
    {

        public static int width;
        public static int height;
        public static int tileWidth = 64;
        private System.Random random;

        public GameObject Path;
        public GameObject Wall1;
        public GameObject Wall2;
        public GameObject Wall3;
        public GameObject Wall4;
        public GameObject Wall5;
        public GameObject Wall6;
        public GameObject Wall7;
        public GameObject HalfBrokenWall;
        public GameObject Door;
        public GameObject Shard;
        public GameObject Key;
        public GameObject SkillBoost;
        public GameObject AttackBoost;

        void Start()
        {
            var laberinto = new Laberinto(51);
            Laberinto.ElLaberinto = laberinto;

            width = laberinto.GetSize();
            height = laberinto.GetSize();
            laberinto.Iniciar();
            random = new System.Random();
            GenerateBoard(laberinto);
        }

        void GenerateBoard(Laberinto laberinto)
        {

            for (int x = 0; x < width; x++)
            {
                for (int y = height - 1; y >= 0; y--)
                {
                    //la funcion lee primero fila y luego columna, es decir, y_filas, x_columnas en coordenadas
                    int value = laberinto.Leer(height - y - 1, x);
                    if (value == 2)
                    {
                        int randomWall = random.Next(0, 12);
                        if (randomWall == 1)
                        {
                            SpawnTile(x * tileWidth, y * tileWidth, Wall1);
                        }
                        else if (randomWall == 2)
                        {
                            SpawnTile(x * tileWidth, y * tileWidth, Wall2);
                        }
                        else if (randomWall == 3)
                        {
                            SpawnTile(x * tileWidth, y * tileWidth, Wall3);
                        }
                        else if (randomWall == 4)
                        {
                            SpawnTile(x * tileWidth, y * tileWidth, Wall4);
                        }
                        else if (randomWall == 5)
                        {
                            SpawnTile(x * tileWidth, y * tileWidth, Wall5);
                        }
                        else if (randomWall == 6)
                        {
                            SpawnTile(x * tileWidth, y * tileWidth, Wall6);
                        }
                        else
                        {
                            SpawnTile(x * tileWidth, y * tileWidth, Wall7);
                        }
                    }
                    else if (value == 11)
                    {
                        SpawnTile(x * tileWidth, y * tileWidth, HalfBrokenWall);
                    }
                    else if (value == 7)
                    {
                        SpawnTile(x * tileWidth, y * tileWidth, Door);
                    }
                    else
                    {
                        SpawnTile(x * tileWidth, y * tileWidth, Path);
                    }

                    if (value == 6)
                    {
                        SpawnShard(x * tileWidth, y * tileWidth);
                    }

                    if (value == 8)
                    {
                        SpawnKey(x * tileWidth, y * tileWidth);
                    }
                    if (value == 9)
                    {
                        SpawnSkillBoost(x * tileWidth, y * tileWidth);
                    }
                    if (value == 10)
                    {
                        SpawnAttackBoost(x * tileWidth, y * tileWidth);
                    }
                }
            }
        }
        //esto genera el tile y lo pone donde va
        public static void SpawnTile(int x, int y, GameObject TileType)
        {
            GameObject tile;
            tile = Instantiate(TileType);
            tile.transform.position = new Vector3(x, y, 0);
            tile.name = "Tile " + x / tileWidth + "," + y / tileWidth;
        }
        public void SpawnShard(int x, int y)
        {
            GameObject shard;
            shard = Instantiate(Shard);
            shard.transform.position = new Vector3(x, y, 0);
            shard.name = "shard " + x / tileWidth + "," + y / tileWidth;
        }
        private void SpawnKey(int x, int y)
        {
            GameObject key;
            key = Instantiate(Key);
            key.transform.position = new Vector3(x, y, 0);
            key.name = "key " + x / tileWidth + "," + y / tileWidth;
        }

        private void SpawnSkillBoost(int x, int y)
        {
            GameObject skillBoost;
            skillBoost = Instantiate(SkillBoost);
            skillBoost.transform.position = new Vector3(x, y, 0);
            skillBoost.name = "skillBoost " + x / tileWidth + "," + y / tileWidth;
        }
        private void SpawnAttackBoost(int x, int y)
        {
            GameObject attackBoost;
            attackBoost = Instantiate(AttackBoost);
            attackBoost.transform.position = new Vector3(x, y, 0);
            attackBoost.name = "attackBoost " + x / tileWidth + "," + y / tileWidth;
        }
    }
}