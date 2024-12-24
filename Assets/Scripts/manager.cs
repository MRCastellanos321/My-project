using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
namespace Tablero
{

    public class Manager : MonoBehaviour
    {

        static Camera[] cameras;

        public static int currentPlayerIndex = 1;
        public static int diceNumber;
        private static System.Random dice = new System.Random();


        public static bool MovimientoValido(Laberinto laberinto, int f, int c)
        {
            if (laberinto.Leer(f, c) == 1)
            {
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

        }

        public static void TurnBegins()
        {
            
            diceNumber = dice.Next(6, 21);
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