using UnityEngine;


namespace Tablero
{

    public class PlayerMovement : MonoBehaviour
    {
        public int playerIndex;

        public static int cellSize = 64;

        private Vector3 targetPosition;

        public static Vector3 lastPosition;


        private Transform[] playersPosition = new Transform[4];
        public Transform player1Position;
        public Transform player2Position;
        public Transform player3Position;
        public Transform player4Position;



        void Start()
        {
            Vector3 temp = transform.position;
            transform.position = new Vector3(temp.x, temp.y, temp.z);


            targetPosition = transform.position;
            Manager.TurnBegins();
        }

        void Update()
        {
            var laberinto = Laberinto.ElLaberinto;

            if (Manager.Instancia.currentPlayerIndex == playerIndex)
            {

                int f = Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][0];
                int c = Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][1];


                if (Manager.diceNumber > 0)
                {

                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        if (Manager.Instancia.MovimientoValido(laberinto, f - 1, c))
                        {
                            FindTarget(Vector3.up);
                            f--;
                            Manager.diceNumber--;

                        }
                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        if (Manager.Instancia.MovimientoValido(laberinto, f + 1, c))
                        {
                            FindTarget(Vector3.down);
                            f++;
                            Manager.diceNumber--;

                        }

                    }

                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (Manager.Instancia.MovimientoValido(laberinto, f, c - 1))
                        {
                            FindTarget(Vector3.left);
                            c--;
                            Manager.diceNumber--;


                        }
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (Manager.Instancia.MovimientoValido(laberinto, f, c + 1))
                        {
                            FindTarget(Vector3.right);

                            c++;
                            Manager.diceNumber--;

                        }
                    }

                    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);//elapsedTime / moveDuration

                    Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][0] = f;
                    Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][1] = c;

                }


                else
                {
                    //esto es para cuando al terminar el turno, dejo de moverse porque ya no es su turno, pero al lerp no le da tiempo a terminar
                    
                    transform.position = targetPosition; 

                    if (Input.GetKeyDown(KeyCode.Space))
                    {

                        Manager.TurnEnds();
                    }
                }



            }

        }

        void FindTarget(Vector3 direction)
        {

            targetPosition += direction * cellSize;

        }
    }
}


