using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tablero
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Use this for initialization

    

    public class SpawnMaze : MonoBehaviour
    {
        
       public int width;
        public int height;
        public int tileWidth = 64; 
        

        public GameObject Camino; 
        public GameObject Bloque2;
       
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
                    SpawnTile(x * tileWidth, y * tileWidth, laberinto.Leer(height - y - 1, x));
                //la funcion lee primero fila y luego columna
                }
            }
        }
        //esto genera el tile y lo pone donde va
        void SpawnTile(int x, int y, int tileType)
        {
            GameObject tile;
            if (tileType == 1)
            {
            tile = Instantiate(Camino);
            }
            else
            {
            tile = Instantiate(Bloque2);
            }
            tile.transform.position = new Vector3(x, y, 0); 
            tile.name = "Tile " + x/tileWidth + "," + y/tileWidth; 
        }


        // Update is called once per frame
        void Update()
        {

        }

    }
}

   