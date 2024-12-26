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

        public GameObject player1;

        public Transform player1Position;

        public GameObject player2;

        public Transform player2Position;

        public GameObject player3;

        public Transform player3Position;

        public GameObject player4;
        public Transform player4Position;

        public Button atackButton1;

        public Button atackButton2;

        public Button atackButton3;
        public Button atackButton4;

        public static Button[] atackButtons = new Button[4];

        private static Transform[] playersPosition = new Transform[4];

        public GameObject selectedSkin;

        public GameObject Player1Sprite;
        private Sprite player1Sprite;

        public static int player1Type;

        //esta funcion se encarga tanto de revisar que la casilla a la que se dirige es un camino como de revisar que no hay otro jugador ahi
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
                */
                    if (Mathf.Approximately(newPosition.x, othersPosition.x) && Mathf.Approximately(newPosition.y, othersPosition.y) && currentPlayerIndex-1 != i)
                    {
                        atackButtons[currentPlayerIndex - 1].gameObject.SetActive(true);
                        return false;
                    }
                }

                return true;
            }

            else
            {
                return false;
            }

        }


        void Start()
        {
            cameras = new Camera[4];

            cameras[0] = GameObject.Find("camera1").GetComponent<Camera>();
            cameras[1] = GameObject.Find("camera2").GetComponent<Camera>();
            cameras[2] = GameObject.Find("camera3").GetComponent<Camera>();
            cameras[3] = GameObject.Find("camera4").GetComponent<Camera>();
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].gameObject.SetActive(i + 1 == currentPlayerIndex);
            }

            player1Sprite = selectedSkin.GetComponent<SpriteRenderer>().sprite;
            Player1Sprite.GetComponent<SpriteRenderer>().sprite = player1Sprite;

            playersPosition[0] = player1Position;
            playersPosition[1] = player2Position;
            playersPosition[2] = player3Position;
            playersPosition[3] = player4Position;

            atackButtons[0] = atackButton1;
            atackButtons[1] = atackButton2;
            atackButtons[2] = atackButton3;
            atackButtons[3] = atackButton4;

            for (int i = 0; i < atackButtons.Length; i++)
            {
                atackButtons[i].gameObject.SetActive(false);
            }


        }


        void Update()
        {

            /*  for (int i = 0; i < playersPosition.Length; i++)
                  if (playersPosition[currentPlayerIndex - 1] == playersPosition[i] && currentPlayerIndex - 1 != i)
                  {
                      playersPosition[currentPlayerIndex - 1].position = PlayerMovement.lastPosition;
                      //Vector3.Lerp(playersPosition[currentPlayerIndex - 1].position, PlayerMovement.lastPosition, Time.deltaTime * 10f);
                  }*/

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