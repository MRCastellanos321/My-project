using UnityEngine;
namespace Tablero
{
    public class SpawnMaze : MonoBehaviour
    {

        public static int width;
        public static int height;
        public static int tileWidth = 64;


        public GameObject Camino;
        public GameObject Bloque2;

        public GameObject Puerta;

        public GameObject Shard;

        void Start()
        {
            var laberinto = new Laberinto(51);
            Laberinto.ElLaberinto = laberinto;

            width = laberinto.Lado();
            height = laberinto.Lado();
            laberinto.Iniciar();
            GenerateBoard(laberinto);
        }

        void GenerateBoard(Laberinto laberinto)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = height - 1; y >= 0; y--)
                {
                    int value = laberinto.Leer(height - y - 1, x);
                    if (value == 2)
                    {
                        SpawnTile(x * tileWidth, y * tileWidth, Bloque2);
                    }
                    else if (value == 7)
                    {
                        SpawnTile(x * tileWidth, y * tileWidth, Puerta);
                    }
                    else
                    {
                        SpawnTile(x * tileWidth, y * tileWidth, Camino);
                    }

                    if (value == 6)
                    {
                        SpawnShard(x * tileWidth, y * tileWidth);
                    }
                    //la funcion lee primero fila y luego columna
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
    }
}
//revisar luego los parches que puse por lo de static
