using Unity.VisualScripting;
using UnityEngine;
namespace Tablero
{
    public class Manager : MonoBehaviour
    {
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

    }
}