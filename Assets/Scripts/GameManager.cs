using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
namespace Tablero
{

    public class Manager : MonoBehaviour
    {

        static Camera[] cameras;

        public static int currentPlayerIndex = 1;
        public static int diceNumber;
        private static System.Random dice = new System.Random();

        //los game object player y su posicion
        public GameObject player1;
        public Transform player1Position;

        public GameObject player2;
        public Transform player2Position;

        public GameObject player3;
        public Transform player3Position;

        public GameObject player4;
        public Transform player4Position;

        public Button attackButton1;

        public Button attackButton2;

        public Button attackButton3;
        public Button attackButton4;

        public static Button[] attackButtons = new Button[4];

        private static Transform[] playersPosition = new Transform[4];

        public static int[][] FilasColumnas = new int[4][];

        // estos son para inicializar los player prefab con la skin seleccionada en el menu
        public GameObject selectedSkin1;
        public GameObject Player1Sprite;
        private Sprite player1Sprite;


        public GameObject selectedSkin2;
        public GameObject Player2Sprite;
        private Sprite player2Sprite;


        public GameObject selectedSkin3;
        public GameObject Player3Sprite;
        private Sprite player3Sprite;


        public GameObject selectedSkin4;

        public GameObject Player4Sprite;
        private Sprite player4Sprite;


        public static int player1Type;

        //esta funcion se encarga tanto de revisar que la casilla a la que se dirige es un camino como de revisar que no hay otro jugador ahi
        public static bool MovimientoValido(Laberinto laberinto, int f, int c, Vector3 direccion)
        {
            if (laberinto.Leer(f, c) == 1)
            {

                for (int i = 0; i < FilasColumnas.Length; i++)

                    if (f == FilasColumnas[i][0] && c == FilasColumnas[i][1] && currentPlayerIndex - 1 != i)
                    {

                        attackButtons[currentPlayerIndex - 1].gameObject.SetActive(true);

                        return false;
                    }

                attackButtons[currentPlayerIndex - 1].gameObject.SetActive(false);
                return true;
            }

            else
            {
                return false;
            }

        }


        void Start()
        {

            //Busca la imagen seleccionada que guardamos en los prefab selected skin y los guarda en el prefab de cada player
            player1Sprite = selectedSkin1.GetComponent<SpriteRenderer>().sprite;
            Player1Sprite.GetComponent<SpriteRenderer>().sprite = player1Sprite;


            player2Sprite = selectedSkin2.GetComponent<SpriteRenderer>().sprite;
            Player2Sprite.GetComponent<SpriteRenderer>().sprite = player2Sprite;

            player3Sprite = selectedSkin3.GetComponent<SpriteRenderer>().sprite;
            Player3Sprite.GetComponent<SpriteRenderer>().sprite = player3Sprite;


            player4Sprite = selectedSkin4.GetComponent<SpriteRenderer>().sprite;
            Player4Sprite.GetComponent<SpriteRenderer>().sprite = player4Sprite;



            cameras = new Camera[4];

            cameras[0] = GameObject.Find("camera1").GetComponent<Camera>();
            cameras[1] = GameObject.Find("camera2").GetComponent<Camera>();
            cameras[2] = GameObject.Find("camera3").GetComponent<Camera>();
            cameras[3] = GameObject.Find("camera4").GetComponent<Camera>();
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].gameObject.SetActive(i + 1 == currentPlayerIndex);
            }


            playersPosition[0] = player1Position;
            playersPosition[1] = player2Position;
            playersPosition[2] = player3Position;
            playersPosition[3] = player4Position;

            attackButtons[0] = attackButton1;
            attackButtons[1] = attackButton2;
            attackButtons[2] = attackButton3;
            attackButtons[3] = attackButton4;

            for (int i = 0; i < attackButtons.Length; i++)
            {
                attackButtons[i].gameObject.SetActive(false);
            }



            //esto inicializa las f y las c segund la posicion en cooredenadas del player
            int[] Player1FC = new int[2];
            Player1FC[0] = 50 - (int)player1Position.position.y / PlayerMovement.cellSize;
            Player1FC[1] = (int)player1Position.position.x / PlayerMovement.cellSize;
            FilasColumnas[0] = Player1FC;

            int[] Player2FC = new int[2];
            Player2FC[0] = 50 - ((int)player2Position.position.y / PlayerMovement.cellSize);
            Player2FC[1] = (int)player2Position.position.x / PlayerMovement.cellSize;
            FilasColumnas[1] = Player2FC;

            int[] Player3FC = new int[2];
            Player3FC[0] = 50 - ((int)player3Position.position.y / PlayerMovement.cellSize);
            Player3FC[1] = (int)player3Position.position.x / PlayerMovement.cellSize;
            FilasColumnas[2] = Player3FC;

            int[] Player4FC = new int[2];
            Player4FC[0] = 50 - ((int)player4Position.position.y / PlayerMovement.cellSize);
            Player4FC[1] = (int)player4Position.position.x / PlayerMovement.cellSize;
            FilasColumnas[3] = Player4FC;

        }


        void Update()
        {


        }
        public static void TurnBegins()
        {
            diceNumber = dice.Next(40, 41);
            Debug.Log("puedes hacer" + diceNumber + "movimientos");
        }

        public static void TurnEnds()
        {
            cameras[currentPlayerIndex - 1].gameObject.SetActive(false);

            if (currentPlayerIndex != 4)
            {
                currentPlayerIndex++;
            }
            else
            {
                currentPlayerIndex = 1;
            }
            cameras[currentPlayerIndex - 1].gameObject.SetActive(true);
            TurnBegins();
        }


    }
}
/*
  public static bool MovimientoValido(Laberinto laberinto, int f, int c, Vector3 direccion)
        {
            if (laberinto.Leer(f, c) == 1)
            {
                Vector3 currentPosition = playersPosition[currentPlayerIndex - 1].position;
                Vector3 newPosition = currentPosition + new Vector3(direccion.x * PlayerMovement.cellSize, direccion.y * PlayerMovement.cellSize, direccion.z);
                // Vector3 newPosition = currentPosition + (direccion * PlayerMovement.cellSize);
                for (int i = 0; i < playersPosition.Length; i++)
                {

                    Vector3 othersPosition = playersPosition[i].position;
                    /* if (newPosition == othersPosition && currentPlayerIndex - 1 != i)
                     {
                         atackButtons[currentPlayerIndex - 1].gameObject.SetActive(true);
                         return false;
                     }
                
                    if (Mathf.Approximately(newPosition.x, othersPosition.x) && Mathf.Approximately(newPosition.y, othersPosition.y) && currentPlayerIndex - 1 != i)
                    {
                        atackButtons[currentPlayerIndex - 1].gameObject.SetActive(true);
                        return false;
                    }
                }

                return true;
            }*/