using JetBrains.Annotations;
using Tablero;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Tablero
{

    public class PlayerMovement : MonoBehaviour
    {
        public int playerIndex;

        public int cellSize = 64;

        private Vector3 targetPosition;

        public Vector3 lastPosition;

        public int f;
        public int c;

        public Transform PlayerA;

        public Transform PlayerB;

        public Transform PlayerC;

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
                    if (transform.position == PlayerA.position)
                    {
                        transform.position = lastPosition;
                        Manager.diceNumber--;

                    }
                     if (transform.position == PlayerB.position)
                    {
                        transform.position = lastPosition;
                        Manager.diceNumber--;

                    }
                     if (transform.position == PlayerC.position)
                    {
                        transform.position = lastPosition;
                        Manager.diceNumber--;

                    }
                    


                }

                else
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Manager.TurnEnds();
                    }
                }


            }

            else if (transform.position == PlayerA.position)
            {
                //llamar a la funcion donde dice cuanto me ataca el player
                //turnos sin participar = lo que retorne
            }

            else if (transform.position == PlayerB.position)
            {
                //llamar a la funcion donde dice cuanto me ataca el player
                //turnos sin participar = lo que retorne
            }
            else if (transform.position == PlayerC.position)
            {
                //llamar a la funcion donde dice cuanto me ataca el player
                //turnos sin participar = lo que retorne
            }


           

        }

         void Move(Vector3 direction)
            {

                targetPosition += direction * cellSize;
            }
    }
}


