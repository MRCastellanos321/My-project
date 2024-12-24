using Tablero;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{

    public class PlayerMovement : MonoBehaviour
    {
        public int playerIndex;

        public int cellSize = 64;

        private Vector3 targetPosition;

        public Vector3 lastPosition;

        public int f;
        public int c;

        void Start()
        {

            Vector3 temp = transform.position;
            transform.position = new Vector3(temp.x, temp.y, temp.z);

            // transform.position = new Vector3(1, 1, 0);


            targetPosition = transform.position;
            
            Manager.TurnBegins();

        }

        void Update()
        {
            var laberinto = Laberinto.ElLaberinto;

            if (Manager.currentPlayerIndex == playerIndex)
            {

                if (Manager.diceNumber > 0)
                {
                    lastPosition = transform.position;
                    //empieza en 1,1
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        if (Manager.MovimientoValido(laberinto, f - 1, c))
                        {
                            Move(Vector3.up);
                            f--;
                            Manager.diceNumber--;
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        if (Manager.MovimientoValido(laberinto, f + 1, c))
                        {
                            Move(Vector3.down);
                            f++;
                            Manager.diceNumber--;
                        }

                    }

                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (Manager.MovimientoValido(laberinto, f, c - 1))
                        {
                            Move(Vector3.left);
                            c--;
                            Manager.diceNumber--;
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (Manager.MovimientoValido(laberinto, f, c + 1))
                        {
                            Move(Vector3.right);

                            c++;
                            Manager.diceNumber--;
                        }
                    }
                    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);

                }
                else
                {
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                    Manager.TurnEnds();
                    }
                }
            }



            void Move(Vector3 direction)
            {

                targetPosition += direction * cellSize;
            }

        }
    }
}


