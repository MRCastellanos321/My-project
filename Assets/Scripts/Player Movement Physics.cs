using System.Collections;
using JetBrains.Annotations;
using Tablero;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Tablero
{

    public class PlayerMovement : MonoBehaviour
    {
        public int playerIndex;

        public static int cellSize = 64;

        private Vector3 targetPosition;

        public static Vector3 lastPosition;

        private Vector3 original;

        
       
        private Transform[] playersPosition = new Transform[4];

        public Transform player1Position;
        public Transform player2Position;
        public Transform player3Position;
        public Transform player4Position;






        void Start()
        {
            /*playersPosition[0] = player1Position;
            playersPosition[1] = player2Position;
            playersPosition[2] = player3Position;
            playersPosition[3] = player4Position;*/

            Vector3 temp = transform.position;
            transform.position = new Vector3(temp.x, temp.y, temp.z);


            targetPosition = transform.position;

            Manager.TurnBegins();

        }

        void Update()
        {



            var laberinto = Laberinto.ElLaberinto;
            Debug.Log(Manager.FilasColumnas[0][0] + "y" + Manager.FilasColumnas[0][1]);
            Debug.Log(Manager.FilasColumnas[1][0] + "y" + Manager.FilasColumnas[1][1]);
            Debug.Log(Manager.FilasColumnas[2][0] + "y" + Manager.FilasColumnas[2][1]);
            Debug.Log(Manager.FilasColumnas[3][0] + "y" + Manager.FilasColumnas[3][1]);





            if (Manager.currentPlayerIndex == playerIndex)
            {

                int f = Manager.FilasColumnas[Manager.currentPlayerIndex - 1][0];
                int c = Manager.FilasColumnas[Manager.currentPlayerIndex - 1][1];

              
                    if (Manager.diceNumber > 0)
                    {

                        if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            if (Manager.MovimientoValido(laberinto, f - 1, c, Vector3.up))
                            {
                                FindTarget(Vector3.up);
                                f--;
                                Manager.diceNumber--;

                            }
                        }
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (Manager.MovimientoValido(laberinto, f + 1, c, Vector3.down))
                            {
                                FindTarget(Vector3.down);
                                f++;
                                Manager.diceNumber--;
                              
                            }

                        }

                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (Manager.MovimientoValido(laberinto, f, c - 1, Vector3.left))
                            {
                                FindTarget(Vector3.left);
                                c--;
                                Manager.diceNumber--;
                                

                            }
                        }
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (Manager.MovimientoValido(laberinto, f, c + 1, Vector3.right))
                            {
                                FindTarget(Vector3.right);

                                c++;
                                Manager.diceNumber--;
                                
                            }
                        }

                        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);//elapsedTime / moveDuration
                       
                        Manager.FilasColumnas[Manager.currentPlayerIndex - 1][0] = f;
                        Manager.FilasColumnas[Manager.currentPlayerIndex - 1][1] = c;

                    }


                    else
                    {
                        transform.position = targetPosition; //pa que ni casualidad termine mal
                        // RoundPosition();

                        if (Input.GetKeyDown(KeyCode.Space))
                        {

                            Manager.TurnEnds();
                        }
                    }



                }

            }
        







        void RoundPosition()
        {
            original = transform.position;
            original.x = Mathf.Round(original.x / 64) * 64;
            original.y = Mathf.Round(original.y / 64) * 64;
            transform.position = original;
        }

        void FindTarget(Vector3 direction)
        {

            targetPosition += direction * cellSize;

        }
        /* IEnumerator MoveToPosition(Vector3 targetPosition)
         {
             onMovement = true;
             float elapsedTime = 0f;
             while (elapsedTime < moveDuration)
             {
                 transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f );//elapsedTime / moveDuration
                 elapsedTime += Time.deltaTime;
                 yield return null;
                 //espera al frame que viene
             }
             transform.position = targetPosition; //pa que ni casualidad termine mal

             onMovement = false;
         }*/

    }
}


