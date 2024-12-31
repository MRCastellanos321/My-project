using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SceneManagement;
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


        private static Transform[] playersPosition = new Transform[4];



        //Los botones de attack
        public Button attackButton1;
        public Button attackButton2;
        public Button attackButton3;
        public Button attackButton4;

        public static Button[] attackButtons = new Button[4];
        public static int nearF;
        public static int nearC;

        //para acceder e iniciar las filas y columnas que permiten la lectura interna de la matriz
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

        //tipo de jugador por int(la forma en que lo devuelve el menu)
        public static int[] selectedTypes;


        //tipo de jugador por string(la forma en que lo trabaja el manager para que sea mas entendible el codigo)
        public static string[] playersType;

        //guarda las coordenadas f y c de la casilla central
        public int[] MazeCenter;


        //Botones de new game, aparecen una vez gana un jugador
        public GameObject NewGameButton1;
        public GameObject NewGameButton2;
        public GameObject NewGameButton3;
        public GameObject NewGameButton4;

        GameObject[] NewGameButtons;


        //Text en pantalla de Victoria
        public TextMeshProUGUI Victory1;
        public TextMeshProUGUI Victory2;
        public TextMeshProUGUI Victory3;
        public TextMeshProUGUI Victory4;

        TextMeshProUGUI[] VictoryMessages;


        public TextMeshProUGUI Trap1;
        public TextMeshProUGUI Trap2;
        public TextMeshProUGUI Trap3;
        public TextMeshProUGUI Trap4;
        TextMeshProUGUI[] TrapMessages;

        void Start()
        {

            NewGameButtons = new GameObject[4] { NewGameButton1, NewGameButton2, NewGameButton3, NewGameButton4 };
            for (int i = 0; i < NewGameButtons.Length; i++)
            {
                NewGameButtons[i].SetActive(false);
            }

            VictoryMessages = new TextMeshProUGUI[4] { Victory1, Victory2, Victory3, Victory4 };
            for (int i = 0; i < VictoryMessages.Length; i++)
            {
               VictoryMessages[i].gameObject.SetActive(false);
            }

            TrapMessages = new TextMeshProUGUI[4] { Trap1, Trap2, Trap3, Trap4 };
            for (int i = 0; i < TrapMessages.Length; i++)
            {
              TrapMessages[i].gameObject.SetActive(false);
            }


            MazeCenter = new int[2] { 25, 25 };

            Debug.Log(MazeCenter[0] + "y" + MazeCenter[1]);

            //Busca la imagen seleccionada que guardamos en los prefab selected skin y los guarda en la instancia de cada player
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

            attackButtons[0] = attackButton1;
            attackButtons[1] = attackButton2;
            attackButtons[2] = attackButton3;
            attackButtons[3] = attackButton4;

            for (int i = 0; i < attackButtons.Length; i++)
            {
                attackButtons[i].gameObject.SetActive(false);
            }



            playersPosition[0] = player1Position;
            playersPosition[1] = player2Position;
            playersPosition[2] = player3Position;
            playersPosition[3] = player4Position;

            //esto inicializa las f y las c segund la posicion en coordenadas del player
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

            selectedTypes = new int[4];
            selectedTypes[0] = MenuFunctions.selectedType1;
            selectedTypes[1] = MenuFunctions.selectedType2;
            selectedTypes[2] = MenuFunctions.selectedType3;
            selectedTypes[3] = MenuFunctions.selectedType4;
            playersType = new string[4];
            for (int i = 0; i < selectedTypes.Length; i++)
            {

                if (selectedTypes[i] == 0)
                {
                    playersType[i] = "Vampiro";
                }
                else if (selectedTypes[i] == 1)
                {
                    playersType[i] = "Bruja";
                }
                else if (selectedTypes[i] == 2)
                {
                    playersType[i] = "Fantasma";
                }
                else if (selectedTypes[i] == 3)
                {
                    playersType[i] = "Hongo";
                }
                else if (selectedTypes[i] == 4)
                {
                    playersType[i] = "Ninfa";
                }

            }

            Vampiro.turnsPassed = 0;
            Bruja.turnsPassed = 0;
            Fantasma.turnsPassed = 0;
            Hongo.turnsPassed = 0;
            Ninfa.turnsPassed = 0;


            Vampiro.attackCoolDown = 0;
            Bruja.attackCoolDown = 0;
            Fantasma.attackCoolDown = 0;
            Hongo.attackCoolDown = 0;
            Ninfa.attackCoolDown = 0;


            Debug.Log(playersType[0]);
            Debug.Log(playersType[1]);
            Debug.Log(playersType[2]);
            Debug.Log(playersType[3]);

        }


        //esta funcion se encarga tanto de revisar que la casilla a la que se dirige es un camino como de revisar que no hay otro jugador ahi
        public static bool MovimientoValido(Laberinto laberinto, int f, int c, Vector3 direccion)
        {
            if (laberinto.Leer(f, c) == 1 || laberinto.Leer(f, c) == 3 || laberinto.Leer(f, c) == 4)
            {
                for (int i = 0; i < FilasColumnas.Length; i++)
                {
                    if (f == FilasColumnas[i][0] && c == FilasColumnas[i][1] && currentPlayerIndex - 1 != i)
                    {
                        if (laberinto.Leer(f, c) == 1)
                        {
                            attackButtons[currentPlayerIndex - 1].gameObject.SetActive(true);
                            nearF = f;
                            nearC = c;
                            //la comprobacion del 1 es para que no pueda atacar al otro jugador si este esta en una trampa
                        }
                        return false;
                    }
                }
                MessageManager.AttackMessage(false, "neutral");
                attackButtons[currentPlayerIndex - 1].gameObject.SetActive(false);
                return true;
            }
            return false;
        }

        public static void TurnBegins()
        {
            diceNumber = dice.Next(40, 41);
            Debug.Log("puedes hacer" + diceNumber + "movimientos");
        }

        /*if (currentPlayerIndex != 4)
                   {
                       currentPlayerIndex++;
                   }
                   else
                   {
                       currentPlayerIndex = 1;
                   }
                   cameras[currentPlayerIndex - 1].gameObject.SetActive(true);
                   TurnBegins();
       */

        public static void TurnEnds()
        {

            int nextPlayerIndex;
            while (true)
            {
                if (currentPlayerIndex != 4)
                {
                    nextPlayerIndex = currentPlayerIndex + 1;
                }
                else
                {
                    nextPlayerIndex = 1;
                }


                if (playersType[nextPlayerIndex - 1] == "Vampiro")
                {
                    if (Vampiro.turnsPassed == 0)
                    {
                        cameras[currentPlayerIndex - 1].gameObject.SetActive(false);
                        currentPlayerIndex = nextPlayerIndex;
                        cameras[currentPlayerIndex - 1].gameObject.SetActive(true);

                        break;
                    }
                    else
                    {
                        Vampiro.turnsPassed--;
                    }

                }


                if (playersType[nextPlayerIndex - 1] == "Bruja")
                {

                    if (Bruja.turnsPassed == 0)
                    {
                        cameras[currentPlayerIndex - 1].gameObject.SetActive(false);
                        currentPlayerIndex = nextPlayerIndex;
                        cameras[currentPlayerIndex - 1].gameObject.SetActive(true);

                        break;
                    }
                    else
                    {
                        Bruja.turnsPassed--;

                    }
                }


                if (playersType[nextPlayerIndex - 1] == "Fantasma")
                {
                    if (Fantasma.turnsPassed == 0)
                    {
                        cameras[currentPlayerIndex - 1].gameObject.SetActive(false);
                        currentPlayerIndex = nextPlayerIndex;
                        cameras[currentPlayerIndex - 1].gameObject.SetActive(true);

                        break;
                    }

                    else
                    {
                        Fantasma.turnsPassed--;
                    }
                }
                if (playersType[nextPlayerIndex - 1] == "Hongo")
                {
                    if (Hongo.turnsPassed == 0)
                    {
                        cameras[currentPlayerIndex - 1].gameObject.SetActive(false);
                        currentPlayerIndex = nextPlayerIndex;
                        cameras[currentPlayerIndex - 1].gameObject.SetActive(true);

                        break;
                    }

                    else
                    {
                        Hongo.turnsPassed--;
                    }
                }
                if (playersType[nextPlayerIndex - 1] == "Ninfa")
                {
                    if (Ninfa.turnsPassed == 0)
                    {
                        cameras[currentPlayerIndex - 1].gameObject.SetActive(false);
                        currentPlayerIndex = nextPlayerIndex;
                        cameras[currentPlayerIndex - 1].gameObject.SetActive(true);

                        break;
                    }

                    else
                    {
                        Ninfa.turnsPassed--;
                    }
                }

                if (Fantasma.attackCoolDown != 0)
                {
                    Fantasma.attackCoolDown--;
                }
                if (Bruja.attackCoolDown != 0)
                {
                    Bruja.attackCoolDown--;
                }
                if (Vampiro.attackCoolDown != 0)
                {
                    Vampiro.attackCoolDown--;
                }
                if (Hongo.attackCoolDown != 0)
                {
                    Hongo.attackCoolDown--;
                }
                if (Ninfa.attackCoolDown != 0)
                {
                    Ninfa.attackCoolDown--;
                }
                //Esto va aqui y no fuera del "for" porque si todos llegaran a estar incapacitados(ej: trampas) entonces es posible que pasen varios turnos para todo el mundo
                //dentro del ciclo

                currentPlayerIndex = nextPlayerIndex;
            }
            TurnBegins();
        }

        public void Attack()
        {

            //esta funcion va a atacar al que yo inente acercarme con "movimiento valido"(nearF y nearC) asi nos deshacemos de los casos donde 
            //la casilla del currentPlayerIndex es adyacente a la de mas de un jugador;

            //Puede que tenga que considerar eliminar las clases de tipo y acceder a las variables de otra forma, la funcion es demasiado larga
            for (int i = 0; i < FilasColumnas.Length; i++)
            {

                if (nearF == FilasColumnas[i][0] && nearC == FilasColumnas[i][1] && currentPlayerIndex - 1 != i)
                {

                    if (playersType[currentPlayerIndex - 1] == "Vampiro")
                    {

                        if (Vampiro.attackCoolDown == 0)
                        {
                            Vampiro.attackCoolDown = +3;
                            if (playersType[i] == "Bruja")
                            {
                                if (Bruja.turnsPassed == 0)
                                {
                                    Debug.Log("ataque una bruja");
                                    Bruja.turnsPassed = +Vampiro.attack;
                                    MessageManager.AttackMessage(true, "Ataque Exitoso!");
                                }

                                else
                                {
                                    MessageManager.AttackMessage(true, "Este jugador ya esta incapacitado");

                                }
                            }

                            else if (playersType[i] == "Fantasma")
                            {
                                if (Fantasma.turnsPassed == 0)
                                {
                                    Fantasma.turnsPassed = +Vampiro.attack;
                                    MessageManager.AttackMessage(true, "Ataque Exitoso!");
                                }
                                else
                                {
                                    MessageManager.AttackMessage(true, "Este jugador ya esta incapacitado");
                                }
                            }

                            else if (playersType[i] == "Hongo")
                            {
                                if (Hongo.turnsPassed == 0)
                                {
                                    Hongo.turnsPassed = +Vampiro.attack;
                                    MessageManager.AttackMessage(true, "Ataque Exitoso!");
                                }
                                else
                                {
                                    MessageManager.AttackMessage(true, "Este jugador ya esta incapacitado");
                                }
                            }

                            else if (playersType[i] == "Ninfa")
                            {
                                if (Ninfa.turnsPassed == 0)
                                {
                                    Ninfa.turnsPassed = +Vampiro.attack;
                                    MessageManager.AttackMessage(true, "Ataque Exitoso!");
                                }
                                else
                                {
                                    MessageManager.AttackMessage(true, "Este jugador ya esta incapacitado");
                                }
                            }
                        }
                        else
                        {
                            MessageManager.AttackMessage(true, "Aun no puedes atacar");
                        }

                    }

                    else if (playersType[currentPlayerIndex - 1] == "Bruja")
                    {
                        if (Bruja.attackCoolDown == 0)
                        {
                            Bruja.attackCoolDown = +3;
                            if (playersType[i] == "Fantasma")
                            {
                                if (Fantasma.turnsPassed == 0)
                                {
                                    Fantasma.turnsPassed = +Bruja.attack;
                                    MessageManager.AttackMessage(true, "Ataque Exitoso!");
                                }
                                else
                                {
                                    MessageManager.AttackMessage(true, "Este jugador ya esta incapacitado");
                                }
                            }
                            else if (playersType[i] == "Hongo")
                            {
                                if (Hongo.turnsPassed == 0)
                                {
                                    Hongo.turnsPassed = +Bruja.attack;
                                    MessageManager.AttackMessage(true, "Ataque Exitoso!");
                                }
                                else
                                {
                                    MessageManager.AttackMessage(true, "Este jugador ya esta incapacitado");
                                }
                            }
                            else if (playersType[i] == "Ninfa")
                            {
                                if (Ninfa.turnsPassed == 0)
                                {
                                    Ninfa.turnsPassed = +Bruja.attack;
                                    MessageManager.AttackMessage(true, "Ataque Exitoso!");
                                }
                                else
                                {
                                    MessageManager.AttackMessage(true, "Este jugador ya esta incapacitado");
                                }
                            }
                        }
                        else
                        {
                            MessageManager.AttackMessage(true, "Aun no puedes atacar");
                        }

                    }

                    else if (playersType[currentPlayerIndex - 1] == "Fantasma")
                    {
                        if (Fantasma.attackCoolDown == 0)
                        {
                            Fantasma.attackCoolDown = +3;
                            if (playersType[i] == "Bruja")
                            {
                                if (Bruja.turnsPassed == 0)
                                {
                                    Bruja.turnsPassed = +Fantasma.attack;
                                    MessageManager.AttackMessage(true, "Ataque Exitoso!");
                                }
                                else
                                {
                                    MessageManager.AttackMessage(true, "Este jugador ya esta incapacitado");
                                }
                            }

                            else if (playersType[i] == "Hongo")
                            {
                                if (Hongo.turnsPassed == 0)
                                {
                                    Hongo.turnsPassed = +Fantasma.attack;
                                    MessageManager.AttackMessage(true, "Ataque Exitoso!");
                                }
                                else
                                {
                                    MessageManager.AttackMessage(true, "Este jugador ya esta incapacitado");
                                }
                            }
                            else if (playersType[i] == "Ninfa")
                            {
                                if (Ninfa.turnsPassed == 0)
                                {
                                    Ninfa.turnsPassed = +Fantasma.attack;
                                    MessageManager.AttackMessage(true, "Ataque Exitoso!");
                                }
                                else
                                {
                                    MessageManager.AttackMessage(true, "Este jugador ya esta incapacitado");
                                }
                            }
                        }
                        else
                        {
                            MessageManager.AttackMessage(true, "Aun no puedes atacar");
                        }

                    }


                    else if (playersType[currentPlayerIndex - 1] == "Hongo")
                    {
                        if (Hongo.attackCoolDown == 0)
                        {
                            Hongo.attackCoolDown = +3;

                            if (playersType[i] == "Bruja")
                            {
                                if (Bruja.turnsPassed == 0)
                                {
                                    Bruja.turnsPassed = +Hongo.attack;
                                    MessageManager.AttackMessage(true, "Ataque Exitoso!");
                                }
                                else
                                {
                                    MessageManager.AttackMessage(true, "Este jugador ya esta incapacitado");
                                }
                            }

                            else if (playersType[i] == "Fantasma")
                            {
                                if (Fantasma.turnsPassed == 0)
                                {
                                    Fantasma.turnsPassed = +Hongo.attack;
                                    MessageManager.AttackMessage(true, "Ataque Exitoso!");
                                }
                                else
                                {
                                    MessageManager.AttackMessage(true, "Este jugador ya esta incapacitado");
                                }
                            }
                            else if (playersType[i] == "Ninfa")
                            {
                                if (Ninfa.turnsPassed == 0)
                                {
                                    Ninfa.turnsPassed = +Hongo.attack;
                                    MessageManager.AttackMessage(true, "Ataque Exitoso!");
                                }
                                else
                                {
                                    MessageManager.AttackMessage(true, "Este jugador ya esta incapacitado");
                                }
                            }
                        }
                        else
                        {
                            MessageManager.AttackMessage(true, "Aun no puedes atacar");
                        }
                    }
                    else if (playersType[currentPlayerIndex - 1] == "Ninfa")
                    {
                        if (Fantasma.attackCoolDown == 0)
                        {
                            Fantasma.attackCoolDown = +3;
                            if (playersType[i] == "Bruja")
                            {
                                if (Bruja.turnsPassed == 0)
                                {
                                    Bruja.turnsPassed = +Ninfa.attack;
                                    MessageManager.AttackMessage(true, "Ataque Exitoso!");
                                }
                                else
                                {
                                    MessageManager.AttackMessage(true, "Este jugador ya esta incapacitado");
                                }
                            }

                            else if (playersType[i] == "Hongo")
                            {
                                if (Hongo.turnsPassed == 0)
                                {
                                    Hongo.turnsPassed = +Ninfa.attack;
                                    MessageManager.AttackMessage(true, "Ataque Exitoso!");
                                }
                                else
                                {
                                    MessageManager.AttackMessage(true, "Este jugador ya esta incapacitado");
                                }
                            }
                            else if (playersType[i] == "Fantasma")
                            {
                                if (Fantasma.turnsPassed == 0)
                                {
                                    Fantasma.turnsPassed = +Ninfa.attack;
                                    MessageManager.AttackMessage(true, "Ataque Exitoso!");
                                }
                                else
                                {
                                    MessageManager.AttackMessage(true, "Este jugador ya esta incapacitado");
                                }
                            }
                        }
                        else
                        {
                            MessageManager.AttackMessage(true, "Aun no puedes atacar");
                        }
                    }
                }
            }
        }

        public void BrujaSkill()
        {
            if (Bruja.skillCoolDown == 0)
            {
                diceNumber = diceNumber * 2;
                Bruja.skillCoolDown = 7;

            }
        }
        public void HongoSkill()
        {

            //Revisa las posiciones en un radio de 4 casillas desde el hongo e incapacita a todos los jugadores "atacables" que encuentre
            if (Hongo.skillCoolDown == 0)
            {
                bool canAttack = false;
                int f = FilasColumnas[currentPlayerIndex - 1][0] - 4;
                int c = FilasColumnas[currentPlayerIndex - 1][1] - 4;

                while (f <= FilasColumnas[currentPlayerIndex - 1][0] + 4)
                {
                    while (c <= FilasColumnas[currentPlayerIndex - 1][1] + 4)
                    {
                        for (int i = 0; i < FilasColumnas.Length; i++)
                        {
                            if (f == FilasColumnas[i][0] && c == FilasColumnas[i][1])
                            {
                                if (i == 1 && Bruja.turnsPassed == 0)
                                {
                                    canAttack = true;
                                    Bruja.turnsPassed += 2;
                                }
                                else if (i == 2 && Fantasma.turnsPassed == 0)
                                {
                                    canAttack = true;
                                    Fantasma.turnsPassed += 2;
                                }
                                else if (i == 4 && Ninfa.turnsPassed == 0)
                                {
                                    canAttack = true;
                                    Ninfa.turnsPassed += 2;
                                }
                                i++;
                            }
                        }
                        c++;
                    }
                    c = 0;
                    f++;
                    //no debe importar si f y c se salen del tablero porque no las estoy usando para acceder a nada, simplemente la comprobacion no va a dar igual
                }


                if (canAttack == true)
                {
                    Hongo.skillCoolDown += 10;
                }

                /*for (int j = 0; j < 5; j++)
                {
                    //revisa diagonal derecha superior
                    /*if (FilasColumnas[currentPlayerIndex - 1][0] + j == FilasColumnas[i][0] && FilasColumnas[currentPlayerIndex - 1][1] + j == FilasColumnas[i + 1][1])
                    {
                    }
                    if (FilasColumnas[currentPlayerIndex - 1][0] - j == FilasColumnas[i][0] && FilasColumnas[currentPlayerIndex - 1][1] - j == FilasColumnas[i + 1][1])
                    {
                    }
                }*/

            }
        }

        public void StartNewGame()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void FellInTrap(Laberinto laberinto)
        {
            if (laberinto.Leer(FilasColumnas[currentPlayerIndex - 1][0], FilasColumnas[currentPlayerIndex - 1][1]) == 3)
            {
                //la trampa  va a hacer al jugador perder un shard
                TrapMessages[currentPlayerIndex - 1].text = "Has caido en la trampa 3"; // texto temporal
                TrapMessages[currentPlayerIndex - 1].gameObject.SetActive(true);
            }
            else
            {
                TrapMessages[currentPlayerIndex - 1].gameObject.SetActive(false);
            }
            if (laberinto.Leer(FilasColumnas[currentPlayerIndex - 1][0], FilasColumnas[currentPlayerIndex - 1][1]) == 4)
            {
                //la trampa va a incapacitarte 3 turnos
                TrapMessages[currentPlayerIndex - 1].text = "Has caido en la trampa 4"; // texto temporal
                TrapMessages[currentPlayerIndex - 1].gameObject.SetActive(true);
            }
            else
            {
                TrapMessages[currentPlayerIndex - 1].gameObject.SetActive(false);
            }

            //mas trampas: teletransportarte al inicio del juego, Teletransportarte junto al jugador al que le toca el turno siguiente y pierdes tu turno
            //disminuye tu numero en los dados durate x turnos
        }
        void Update()
        {
            var laberinto = Laberinto.ElLaberinto;
            Debug.Log(FilasColumnas[currentPlayerIndex - 1][0] + "y" + FilasColumnas[currentPlayerIndex - 1][1]);

            //hay que programar aun la otra condicion de final
            if (FilasColumnas[currentPlayerIndex - 1][0] == MazeCenter[0] && FilasColumnas[currentPlayerIndex - 1][1] == MazeCenter[1])
            {
                NewGameButtons[currentPlayerIndex - 1].SetActive(true);
                VictoryMessages[currentPlayerIndex - 1].gameObject.SetActive(true);

            }
            FellInTrap(laberinto);

        }

        /*public static void TurnEnds()
               {

                   int nextPlayerIndex;
                   while (true)
                   {
                       if (currentPlayerIndex != 4)
                       {
                           nextPlayerIndex = currentPlayerIndex + 1;
                       }
                       else
                       {
                           nextPlayerIndex = 1;
                       }


                       if (playersType[nextPlayerIndex - 1] == "Vampiro")
                       {
                           if (Vampiro.turnsPassed == 0)
                           {
                               cameras[currentPlayerIndex - 1].gameObject.SetActive(false);
                               currentPlayerIndex = nextPlayerIndex;
                               cameras[currentPlayerIndex - 1].gameObject.SetActive(true);
                               TurnBegins();
                               break;
                           }
                           else
                           {
                               Vampiro.turnsPassed--;
                           }

                       }


                       if (playersType[nextPlayerIndex - 1] == "Bruja")
                       {

                           if (Bruja.turnsPassed == 0)
                           {
                               cameras[currentPlayerIndex - 1].gameObject.SetActive(false);
                               currentPlayerIndex = nextPlayerIndex;
                               cameras[currentPlayerIndex - 1].gameObject.SetActive(true);
                               TurnBegins();
                               break;
                           }
                           else
                           {
                               Bruja.turnsPassed--;
                           }
                       }


                       if (playersType[nextPlayerIndex - 1] == "Fantasma")
                       {
                           if (Fantasma.turnsPassed == 0)
                           {
                               cameras[currentPlayerIndex - 1].gameObject.SetActive(false);
                               currentPlayerIndex = nextPlayerIndex;
                               cameras[currentPlayerIndex - 1].gameObject.SetActive(true);
                               TurnBegins();
                               break;
                           }

                           else
                           {
                               Fantasma.turnsPassed--;
                           }
                       }

                   }
               }*/

    }
}
