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
        public int width = 10;
        public int height = 10; 
        public int tileWidth = 32; 
        

        public GameObject tilePrefab2; 

        private int[,] board; 

        void Start()
        {
            board = new int[width, height];
            GenerateBoard();
        }

        void GenerateBoard()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                   
                    board[x, y] = UnityEngine.Random.Range(0, 4); //falta progrma esto segun el tile q quiera poner

                    
                    SpawnTile(x * tileWidth, y * tileWidth, board[x, y]);
                }
            }
        }
        //esto genera el tile y lo pone donde va
        void SpawnTile(int x, int y, int tileType)
        {
            
            GameObject tile = Instantiate(tilePrefab2);
           
            tile.transform.position = new Vector3(x, y, 0); 
            tile.name = "Tile " + x + "," + y; 
        }




        // Update is called once per frame
        void Update()
        {

        }

    }
}

   