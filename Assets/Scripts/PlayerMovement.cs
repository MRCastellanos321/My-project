using UnityEngine;


namespace Tablero
{
    public class PlayerMovement : MonoBehaviour
    {
        public int playerIndex;

        public static int cellSize = 64;

        private Vector3 targetPosition;

        public static Vector3 lastPosition;
        void Start()
        {
            Vector3 temp = transform.position;
            transform.position = new Vector3(temp.x, temp.y, temp.z);

            targetPosition = transform.position;
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
                    if (Bruja.onTeleport == true)
                    {
                        targetPosition = new Vector3(Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][1] * cellSize, (Laberinto.ElLaberinto.GetSize() - Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][0] - 1) * cellSize, 0);
                        Bruja.onTeleport = false;
                    }
                    else
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
                    }
                    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
                    Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][0] = f;
                    Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][1] = c;

                }


                else
                {
                    //esto es para cuando al terminar el turno, dejo de moverse porque ya no es su turno, pero al lerp no le da tiempo a terminar

                    transform.position = targetPosition;
                }
            }
        }

        void FindTarget(Vector3 direction)
        {

            targetPosition += direction * cellSize;

        }

        public void TeleportTarget(int f, int c)
        {
            targetPosition = Manager.playersPosition[Manager.Instancia.currentPlayerIndex - 1].position = new Vector3(c * cellSize, (Laberinto.ElLaberinto.GetSize() - f - 1) * cellSize, 0);
        }
    }
}


