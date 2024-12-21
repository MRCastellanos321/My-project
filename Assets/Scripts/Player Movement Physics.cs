using Tablero;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{

    public class PlayerMovement : MonoBehaviour
    {

        /* void Start()
         {
             transform.position = new Vector3(1, 1, 0);
         }

         float posCellX = 0;
         float posCellY = 0;

         void Update()
         {
             float directionX = Input.GetAxis("Horizontal");
             if (directionX == 0)
             {



             }
             float directionY = Input.GetAxis("Vertical");
             posCellX += directionX;
             posCellY += directionY;
             transform.position = new Vector3(posCellX * 64, posCellY * 64, 0);*/


        public int cellSize = 1;
        private Vector3 targetPosition;

        public Vector3 lastPosition;

        private int f;
        private int c;

        void Start()
        {

            Vector3 temp = transform.position;
            transform.position = new Vector3(temp.x, temp.y, temp.z);

            // transform.position = new Vector3(1, 1, 0);


            targetPosition = transform.position;
            f = 5;
            c = 1;

        }

        void Update()
        {
            var laberinto = Laberinto.ElLaberinto;
            lastPosition = transform.position;



            //empieza en 1,1
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (Manager.MovimientoValido(laberinto, f - 1, c))
                {
                    Move(Vector3.up);
                    f--;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (Manager.MovimientoValido(laberinto, f + 1, c))
                {
                    Move(Vector3.down);
                    f++;
                }

            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (Manager.MovimientoValido(laberinto, f, c - 1))
                {
                    Move(Vector3.left);
                    c--;
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (Manager.MovimientoValido(laberinto, f, c + 1))
                {
                    Move(Vector3.right);

                    c++;
                }
            }
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);








            void Move(Vector3 direction)
            {

                targetPosition += direction * cellSize;
            }

        }
    }
}


