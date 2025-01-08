using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
namespace Tablero
{
    public class Manager : MonoBehaviour
    {

        public static Manager Instancia;

        static Camera[] cameras;

        public int currentPlayerIndex = 1;
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

        public static int nearF;
        public static int nearC;

        public static int nearDoorF;
        public static int nearDoorC;

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

        private GameObject[] playersSprite;

        //tipo de jugador por int(la forma en que lo devuelve el menu)
        public static int[] selectedTypes;
        //tipo de jugador por clase e interfaz
        public static characterInterface[] playersType;


        public Sprite humanSprite1;
        public Sprite humanSprite2;
        public Sprite humanSprite3;
        public Sprite humanSprite4;
        private Sprite[] humanSprites;



        //guarda las coordenadas f y c de la casilla central
        public int[] MazeCenter;



        public Button NewGameButton;
        public Button attackButton;
        public Button skillButton;
        public Button useKeyButton;

        public TextMeshProUGUI unvalidVictoryText;
        public TextMeshProUGUI trapText;
        public TextMeshProUGUI changeTurnText;
        public TextMeshProUGUI validAttackText;
        public TextMeshProUGUI shardCollectionText;
        public TextMeshProUGUI RemainingMovesText;
        public TextMeshProUGUI skillEffectText;
        public TextMeshProUGUI underTrapEffectText;
        public TextMeshProUGUI turnInHumanText;

        private int messageShowCount = 0;
        public GameObject OpenDoor;
        public GameObject UnactiveTrap;

        public GameObject WinnerPlayer;
        public bool[] Human;
        private bool onTrap = false;

        void Start()
        {
            Instancia = this;
            if (!MenuFunctions.EnteredMenu)
            {
                SceneManager.LoadScene("MainMenu");
            }
            //si cualquier slected type es igual a otro significa que el juego no se esta ejecutando desde el menu
            //si el juego no se ejecuta desde el menu, todos los selected types van a ser igual a cero pero el sprite sera igual
            //al mismo que se uso antes. Entonces esto es una medida extra para cuando se ejecute desde la escena del juego
            //para que se cargue primero el menu


            selectedTypes = new int[4];
            selectedTypes[0] = MenuFunctions.selectedType1;
            selectedTypes[1] = MenuFunctions.selectedType2;
            selectedTypes[2] = MenuFunctions.selectedType3;
            selectedTypes[3] = MenuFunctions.selectedType4;


            playersType = new characterInterface[4];
            for (int i = 0; i < selectedTypes.Length; i++)
            {
                if (selectedTypes[i] == 0)
                {
                    playersType[i] = new Vampiro();
                }
                else if (selectedTypes[i] == 1)
                {
                    playersType[i] = new Bruja();
                }
                else if (selectedTypes[i] == 2)
                {
                    playersType[i] = new Fantasma();
                }
                else if (selectedTypes[i] == 3)
                {
                    playersType[i] = new Hongo();
                }
                else if (selectedTypes[i] == 4)
                {
                    playersType[i] = new Ninfa();
                }
            }

            //Busca la imagen seleccionada que guardamos en los prefab selected skin y los guarda en la instancia de cada player
            player1Sprite = selectedSkin1.GetComponent<SpriteRenderer>().sprite;
            Player1Sprite.GetComponent<SpriteRenderer>().sprite = player1Sprite;


            player2Sprite = selectedSkin2.GetComponent<SpriteRenderer>().sprite;
            Player2Sprite.GetComponent<SpriteRenderer>().sprite = player2Sprite;

            player3Sprite = selectedSkin3.GetComponent<SpriteRenderer>().sprite;
            Player3Sprite.GetComponent<SpriteRenderer>().sprite = player3Sprite;


            player4Sprite = selectedSkin4.GetComponent<SpriteRenderer>().sprite;
            Player4Sprite.GetComponent<SpriteRenderer>().sprite = player4Sprite;

            playersSprite = new GameObject[4];
            playersSprite[0] = Player1Sprite;
            playersSprite[1] = Player2Sprite;
            playersSprite[2] = Player3Sprite;
            playersSprite[3] = Player4Sprite;

            humanSprites = new Sprite[4];
            humanSprites[0] = humanSprite1;
            humanSprites[1] = humanSprite2;
            humanSprites[2] = humanSprite3;
            humanSprites[3] = humanSprite4;

            Human = new bool[4];
            for (int i = 0; i < Human.Length; i++)
            {
                Human[i] = false;
            }

            NewGameButton.gameObject.SetActive(false);
            useKeyButton.gameObject.SetActive(false);

            unvalidVictoryText.gameObject.SetActive(false);
            trapText.gameObject.SetActive(false);
            underTrapEffectText.gameObject.SetActive(false);
            changeTurnText.gameObject.SetActive(false);
            validAttackText.gameObject.SetActive(false);
            skillEffectText.gameObject.SetActive(false);
            turnInHumanText.gameObject.SetActive(false);

            MazeCenter = new int[2] { 25, 25 };

            cameras = new Camera[4];

            cameras[0] = GameObject.Find("camera1").GetComponent<Camera>();
            cameras[1] = GameObject.Find("camera2").GetComponent<Camera>();
            cameras[2] = GameObject.Find("camera3").GetComponent<Camera>();
            cameras[3] = GameObject.Find("camera4").GetComponent<Camera>();
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].gameObject.SetActive(i + 1 == currentPlayerIndex);
            }

            attackButton.gameObject.SetActive(false);

            playersPosition[0] = player1Position;
            playersPosition[1] = player2Position;
            playersPosition[2] = player3Position;
            playersPosition[3] = player4Position;

            //esto inicializa las f y las c segun la posicion en coordenadas del player
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
            TurnBegins();

        }


        //esta funcion se encarga tanto de revisar que la casilla a la que se dirige es un camino como de revisar que no hay otro jugador ahi
        public bool MovimientoValido(Laberinto laberinto, int f, int c)
        {
            int posNumber = laberinto.Leer(f, c);
            if (posNumber != 2)
            {
                if (posNumber != 7)
                {
                    for (int i = 0; i < FilasColumnas.Length; i++)
                    {
                        if (f == FilasColumnas[i][0] && c == FilasColumnas[i][1] && currentPlayerIndex - 1 != i)
                        {
                            if (laberinto.Leer(f, c) == 1 || laberinto.Leer(f, c) == 6 || laberinto.Leer(f, c) == 8)
                            {
                                attackButton.gameObject.SetActive(true);
                                nearF = f;
                                nearC = c;
                                //la comprobacion del 1 es para que no pueda atacar al otro jugador si este esta en una trampa
                            }
                            return false;
                        }
                    }
                    validAttackText.gameObject.SetActive(false);
                    attackButton.gameObject.SetActive(false);
                    useKeyButton.gameObject.SetActive(false);
                    onTrap = false;
                    return true;
                }
                else
                {
                    validAttackText.gameObject.SetActive(false);
                    attackButton.gameObject.SetActive(false);

                    if (playersType[currentPlayerIndex - 1].GetCollectedKeys() != 0)
                    {
                        nearDoorF = f;
                        nearDoorC = c;
                        useKeyButton.gameObject.SetActive(true);
                    }
                }
            }
            return false;
        }

        public void TurnBegins()
        {
            diceNumber = dice.Next(40, 41);
            underTrapEffectText.gameObject.SetActive(false);
            trapText.gameObject.SetActive(false);
            messageShowCount = 0;
            //esto no se hace en el turnEnds porque entonces si se te acaba el turno al mismo tiempo que caes en una trampa no se mostraria
        }

        public void TurnEnds()
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

                if (playersType[nextPlayerIndex - 1].GetTurnsPassed() == 0)
                {
                    cameras[currentPlayerIndex - 1].gameObject.SetActive(false);
                    currentPlayerIndex = nextPlayerIndex;
                    cameras[currentPlayerIndex - 1].gameObject.SetActive(true);
                    if (playersType[currentPlayerIndex - 1].GetAttackCoolDown() != 0)
                    {
                        playersType[currentPlayerIndex - 1].SetAttackCoolDown(-1);
                    }
                    if (playersType[currentPlayerIndex - 1].GetSkillCoolDown() != 0)
                    {
                        playersType[currentPlayerIndex - 1].SetSkillCoolDown(-1);
                    }
                    break;
                }
                else
                {
                    playersType[nextPlayerIndex - 1].SetTurnsPassed(-1);
                    currentPlayerIndex = nextPlayerIndex;

                    if (playersType[currentPlayerIndex - 1].GetAttackCoolDown() != 0)
                    {
                        playersType[currentPlayerIndex - 1].SetAttackCoolDown(-1);
                    }
                    if (playersType[currentPlayerIndex - 1].GetSkillCoolDown() != 0)
                    {
                        playersType[currentPlayerIndex - 1].SetSkillCoolDown(-1);
                    }
                    //los cambios en los cooldown va a ocurrir cuando te toca aunque se te salte en turno}
                }
            }
            TurnBegins();
        }
        public static void ChangeMessage(string message, TextMeshProUGUI textObject)
        {
            textObject.gameObject.SetActive(true);
            textObject.text = message;
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
                    if (playersType[currentPlayerIndex - 1].GetAttackCoolDown() == 0)
                    {
                        if (playersType[i].GetTurnsPassed() == 0)
                        {

                            if (playersType[i].GetAttackInmunity() == 0)
                            {
                                playersType[i].SetTurnsPassed(playersType[currentPlayerIndex - 1].GetAttack());
                                ChangeMessage("Ataque Exitoso!", validAttackText);
                            }
                            else
                            {
                                ChangeMessage("Ha evadido tu ataque", validAttackText);
                                playersType[i].SetAttackInmunity(-1);
                            }
                            playersType[currentPlayerIndex - 1].SetAttackCoolDown(3);
                        }

                        else
                        {
                            ChangeMessage("Ya esta incapacitado", validAttackText);
                        }
                    }
                    else
                    {
                        ChangeMessage("Aun no puedes atacar", validAttackText);
                    }
                    break; // porque cuando encuentre quien es el jugador al que me acerque para atacar ya no hace falta revisar los otros
                }
            }
        }


        public void StartNewGame()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void FellInTrap(Laberinto laberinto)
        {
            int number = laberinto.Leer(FilasColumnas[currentPlayerIndex - 1][0], FilasColumnas[currentPlayerIndex - 1][1]);

            if (number == 3 || number == 4 || number == 5)
            {
                if (onTrap == false)
                {
                    int f = FilasColumnas[currentPlayerIndex - 1][0];
                    int c = FilasColumnas[currentPlayerIndex - 1][1];
                    onTrap = true;
                    messageShowCount = 0;
                    if (playersType[currentPlayerIndex - 1].GetTrapInmunity() == 0)
                    {
                        if (number == 3 && !Human[currentPlayerIndex - 1])
                        {
                            ChangeMessage("Has activado una trampa", trapText);
                            if (playersType[currentPlayerIndex - 1].GetCollectedShards() != 0)
                            {
                                playersType[currentPlayerIndex - 1].SetCollectedShards(-1); // texto temporal     
                                laberinto.SetPosValue(f, c, 1);
                                SpawnMaze.SpawnTile(c * SpawnMaze.tileWidth, (Laberinto.ElLaberinto.GetSize() - f - 1) * SpawnMaze.tileWidth, UnactiveTrap);
                                //desactiva la trampa luego de activarla
                                ChangeMessage("Perdiste un fragmento", underTrapEffectText);
                            }
                            else
                            {
                                ChangeMessage("No tienes fragmentos que perder", underTrapEffectText);
                            }
                        }

                        else if (number == 4)
                        {
                            ChangeMessage("Has activado una trampa", trapText);
                            ChangeMessage("Te perderas 3 turnos", underTrapEffectText);
                            playersType[currentPlayerIndex - 1].SetTurnsPassed(3);
                            SpawnMaze.SpawnTile(c * SpawnMaze.tileWidth, (Laberinto.ElLaberinto.GetSize() - f - 1) * SpawnMaze.tileWidth, UnactiveTrap);
                            laberinto.SetPosValue(f, c, 1);
                            diceNumber = 0;
                        }

                        else if (number == 5 && !Human[currentPlayerIndex - 1])
                        {
                            ChangeMessage("Has activado una trampa", trapText);// texto temporal
                            ChangeMessage("+3 turnos antes de usar tu poder", underTrapEffectText);
                            playersType[currentPlayerIndex - 1].SetSkillCoolDown(3);

                            laberinto.SetPosValue(f, c, 1);
                            SpawnMaze.SpawnTile(c * SpawnMaze.tileWidth, (Laberinto.ElLaberinto.GetSize() - f - 1) * SpawnMaze.tileWidth, UnactiveTrap);
                        }
                    }
                    else
                    {
                        playersType[currentPlayerIndex - 1].SetTrapInmunity(-1);
                        ChangeMessage("Has evadido una trampa", trapText);
                        laberinto.SetPosValue(f, c, 1);
                        SpawnMaze.SpawnTile(c * SpawnMaze.tileWidth, (Laberinto.ElLaberinto.GetSize() - f - 1) * SpawnMaze.tileWidth, UnactiveTrap);
                    }
                }
                //puede que haya que hacer mas especificaciones para cuando el jugador se convierta en humano
            }
            //mas trampas: teletransportarte al inicio del juego, Teletransportarte junto al jugador al que le toca el turno siguiente y pierdes tu turno
            //disminuye tu numero en los dados durate x turnos
            //el onTrap es para que no te quite el cartel de has caido en una trampa aunque no te hayas movido
            //ahora hay que revisar que pasa si hay dos trampas seguidas y lo q esta haciendo el effect text
            //si quiero que se te afecte en dos trampas segudidas debo cambiar el on trap true y false dentro del movimiento valido para que se cambie cuando se mueva(solo en retorno true)
            else
            {
                onTrap = false;
            }
        }
        public void SkillButton()
        {
            playersType[currentPlayerIndex - 1].Skill();
        }
        public void TurnInHuman()
        {
            playersSprite[currentPlayerIndex - 1].GetComponent<SpriteRenderer>().sprite = humanSprites[currentPlayerIndex - 1];
            playersType[currentPlayerIndex - 1].SetSkillCoolDown(9999);
            turnInHumanText.gameObject.SetActive(true);
            Human[currentPlayerIndex - 1] = true;
        }
        public void UseKeyButton()
        {
            playersType[currentPlayerIndex - 1].SetCollectedKeys(-1);
            Laberinto.ElLaberinto.SetPosValue(nearDoorF, nearDoorC, 1);
            SpawnMaze.SpawnTile(nearDoorC * SpawnMaze.tileWidth, (Laberinto.ElLaberinto.GetSize() - nearDoorF - 1) * SpawnMaze.tileWidth, OpenDoor);
        }
        void Update()
        {
            var laberinto = Laberinto.ElLaberinto;
            ChangeMessage($"{diceNumber}", RemainingMovesText);
            if (Human[currentPlayerIndex - 1] == false)
            {
                ChangeMessage($"{playersType[currentPlayerIndex - 1].GetCollectedShards()} Fragmentos de Alma", shardCollectionText);
            }
            else
            {
                shardCollectionText.gameObject.SetActive(false);
            }

            if (FilasColumnas[currentPlayerIndex - 1][0] == MazeCenter[0] && FilasColumnas[currentPlayerIndex - 1][1] == MazeCenter[1])
            {
                if (Human[currentPlayerIndex - 1] == true)
                {
                    WinnerPlayer.GetComponent<SpriteRenderer>().sprite = playersSprite[currentPlayerIndex - 1].GetComponent<SpriteRenderer>().sprite;
                    SceneManager.LoadScene("WinScreen");
                    //NewGameButton.gameObject.SetActive(true);
                    // ChangeMessage("Has ganado!", unvalidVictoryText);
                }
                else
                {
                    unvalidVictoryText.gameObject.SetActive(true);
                }
            }
            else
            {
                unvalidVictoryText.gameObject.SetActive(false);
            }


            if (diceNumber == 0)
            {
                ChangeMessage("Presiona espacio para pasar el turno", changeTurnText);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    TurnEnds();
                }
            }
            else
            {
                changeTurnText.gameObject.SetActive(false);
            }


            if (playersType[currentPlayerIndex - 1].GetSkillCoolDown() == 0)
            {
                skillButton.gameObject.SetActive(true);
            }
            else
            {
                skillButton.gameObject.SetActive(false);
            }

            Instancia.FellInTrap(laberinto);

            if (Input.anyKeyDown)
            {
                skillEffectText.gameObject.SetActive(false);
                messageShowCount++;
                if (messageShowCount == 4)
                {
                    turnInHumanText.gameObject.SetActive(false);
                    underTrapEffectText.gameObject.SetActive(false);
                    trapText.gameObject.SetActive(false);
                    messageShowCount = 0;
                }

            }

        }

    }
}
