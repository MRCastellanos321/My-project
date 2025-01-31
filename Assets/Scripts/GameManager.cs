using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
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


        public static Transform[] playersPosition = new Transform[4];

        public static int nearF;
        public static int nearC;

        public static int nearDoorF;
        public static int nearDoorC;

        public static int nearBrokenWallF;
        public static int nearBrokenWallC;

        //para acceder e iniciar las filas y columnas que permiten la lectura interna de la matriz
        public static int[][] FilasColumnas;



        // estos son para inicializar los player prefab con la skin seleccionada en el menu
        public GameObject Player1Sprite;
        public GameObject Player2Sprite;

        public GameObject selectedSkin3;
        public GameObject Player3Sprite;


        public GameObject selectedSkin4;
        public GameObject Player4Sprite;

        public GameObject[] playersSprite;

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
        private int[] MazeCenter;

        public Button NewGameButton;
        public Button attackButton;
        public Button skillButton;
        public Button useKeyButton;
        public Button breakWallButton;
        public Button passTurnButton;

        public Image keyOwnershipImage;
        public Image diceEffectImage;
        public Image attackCoolDownImage;
        public Image MapTrapEffectImage;

        //public TextMeshProUGUI unvalidVictoryText;
        public TextMeshProUGUI trapText;
        public TextMeshProUGUI changeTurnText;
        public TextMeshProUGUI validAttackText;
        public TextMeshProUGUI shardCollectionText;
        public TextMeshProUGUI remainingMovesText;
        public TextMeshProUGUI skillEffectText;
        public TextMeshProUGUI underTrapEffectText;
        public TextMeshProUGUI turnInHumanText;
        public TextMeshProUGUI skillCoolDownText;
        public TextMeshProUGUI unvalidVictoryText;
        private int messageShowCount = 0;
        public GameObject OpenDoor;
        public GameObject Path;
        public GameObject UnactiveTrap;

        public GameObject WinnerPlayer;
        public bool[] Human;
        public Sprite[] Player1SpriteList;
        public Sprite[] Player2SpriteList;
        public Sprite[] Player3SpriteList;
        public Sprite[] Player4SpriteList;

        void Start()
        {
            Instancia = this;
            if (!MenuFunctions.EnteredMenu)
            {
                SceneManager.LoadScene("MainMenu");
            }
            //Si el juego no se ejecuta desde el menu, el sprite sera igual al mismo que de la ultima partida, pero no
            //estara en correspondencia con el selected type int. Entonces esto es una medida extra para cuando se ejecute
            // desde la escena del juego se cargue primero el menu

            playersPosition[0] = player1Position;
            playersPosition[1] = player2Position;
            playersPosition[2] = player3Position;
            playersPosition[3] = player4Position;

            //Jugador1 fila final  columna del medio
            //Jugador2 columna 0, fila del medio, 
            //Jugador3 columna final, fila del medio
            //Jugador4 fila 0, columna del medio
            int[] Player1FC = new int[2];
            Player1FC[0] = Laberinto.ElLaberinto.GetSize() - 2;
            Player1FC[1] = (Laberinto.ElLaberinto.GetSize() - 1) / 2;

            int[] Player2FC = new int[2];
            Player2FC[0] = (Laberinto.ElLaberinto.GetSize() - 1) / 2;
            Player2FC[1] = 1;

            int[] Player3FC = new int[2];
            Player3FC[0] = (Laberinto.ElLaberinto.GetSize() - 1) / 2;
            Player3FC[1] = Laberinto.ElLaberinto.GetSize() - 2;

            int[] Player4FC = new int[2];
            Player4FC[0] = 1;
            Player4FC[1] = (Laberinto.ElLaberinto.GetSize() - 1) / 2;
            FilasColumnas = new int[4][] { Player1FC, Player2FC, Player3FC, Player4FC };

            //inicia las coordenadas de los jugadores segun su fila y columna inicial
            for (int i = 0; i < playersPosition.Length; i++)
            {
                playersPosition[i].position = new Vector3(FilasColumnas[i][1] * PlayerMovement.cellSize, (Laberinto.ElLaberinto.GetSize() - FilasColumnas[i][0] - 1) * PlayerMovement.cellSize, 0);
            }

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
                else if (selectedTypes[i] == 5)
                {
                    playersType[i] = new Dragon();
                }
                else
                {
                    playersType[i] = new Vidente();
                }
            }

            //Busca la imagen en las listas segun el selected type del menu y los guarda en la instancia de cada player
            Player1Sprite.GetComponent<SpriteRenderer>().sprite = Player1SpriteList[selectedTypes[0]];
            Player2Sprite.GetComponent<SpriteRenderer>().sprite = Player2SpriteList[selectedTypes[1]];
            Player3Sprite.GetComponent<SpriteRenderer>().sprite = Player3SpriteList[selectedTypes[2]];
            Player4Sprite.GetComponent<SpriteRenderer>().sprite = Player4SpriteList[selectedTypes[3]];

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
            breakWallButton.gameObject.SetActive(false);


            unvalidVictoryText.gameObject.SetActive(false);
            trapText.gameObject.SetActive(false);
            underTrapEffectText.gameObject.SetActive(false);
            changeTurnText.gameObject.SetActive(false);
            validAttackText.gameObject.SetActive(false);
            skillEffectText.gameObject.SetActive(false);
            turnInHumanText.gameObject.SetActive(false);
            skillCoolDownText.gameObject.SetActive(false);
            attackButton.gameObject.SetActive(false);

            MazeCenter = new int[2] { (Laberinto.ElLaberinto.GetSize() - 1) / 2, (Laberinto.ElLaberinto.GetSize() - 1) / 2 };

            cameras = new Camera[4];

            cameras[0] = GameObject.Find("camera1").GetComponent<Camera>();
            cameras[1] = GameObject.Find("camera2").GetComponent<Camera>();
            cameras[2] = GameObject.Find("camera3").GetComponent<Camera>();
            cameras[3] = GameObject.Find("camera4").GetComponent<Camera>();
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].gameObject.SetActive(i + 1 == currentPlayerIndex);
            }
            keyOwnershipImage.color = Color.gray;
            TurnBegins();
        }

        //esta funcion se encarga tanto de revisar que la casilla a la que se dirige es un camino como de revisar que no hay otro jugador ahi
        public bool ValidMovement(Laberinto laberinto, int f, int c)
        {
            int posNumber = laberinto.Read(f, c);
            if (posNumber != 2)
            {
                if (posNumber != 13 && posNumber != 11)
                {
                    //13 es una puerta y 11 es una pared rompible, la separacion de las comprobaciones es pq ellas
                    //no necesitan buscar jugadores en su casilla y porque necesitan activar sus respectivos botones
                    validAttackText.gameObject.SetActive(false);
                    useKeyButton.gameObject.SetActive(false);
                    breakWallButton.gameObject.SetActive(false);
                    unvalidVictoryText.gameObject.SetActive(false);
                    for (int i = 0; i < FilasColumnas.Length; i++)
                    {
                        if (f == FilasColumnas[i][0] && c == FilasColumnas[i][1] && currentPlayerIndex - 1 != i)
                        {

                            attackButton.gameObject.SetActive(true);
                            nearF = f;
                            nearC = c;
                            return false;
                        }
                    }
                    if (posNumber == 14)
                    {
                        if (Instancia.Human[Instancia.currentPlayerIndex - 1])
                        {
                            WinnerPlayer.GetComponent<SpriteRenderer>().sprite = Instancia.playersSprite[Instancia.currentPlayerIndex - 1].GetComponent<SpriteRenderer>().sprite;
                            SceneManager.LoadScene("WinScreen");
                        }
                        else
                        {
                            unvalidVictoryText.gameObject.SetActive(true);
                        }
                    }
                    attackButton.gameObject.SetActive(false);
                    return true;
                }
                else if (posNumber == 13)
                {
                    validAttackText.gameObject.SetActive(false);
                    attackButton.gameObject.SetActive(false);
                    breakWallButton.gameObject.SetActive(false);

                    if (playersType[currentPlayerIndex - 1].GetCollectedKeys() != 0)
                    {
                        nearDoorF = f;
                        nearDoorC = c;
                        useKeyButton.gameObject.SetActive(true);
                    }
                }

                else if (posNumber == 11)
                {
                    validAttackText.gameObject.SetActive(false);
                    attackButton.gameObject.SetActive(false);

                    nearBrokenWallF = f;
                    nearBrokenWallC = c;
                    breakWallButton.gameObject.SetActive(true);
                }
            }
            return false;
        }

        public void TurnBegins()
        {
            //el text del skill no se toca aqui porque ya se cambio al recibir la tecla espacio para cambiar el turno
            diceNumber = dice.Next(10, 21);
            if (playersType[currentPlayerIndex - 1].GetDiceEffect() != 0)
            {
                diceNumber /= 2;
            }
            useKeyButton.gameObject.SetActive(false);
            attackButton.gameObject.SetActive(false);
            breakWallButton.gameObject.SetActive(false);
            underTrapEffectText.gameObject.SetActive(false);
            trapText.gameObject.SetActive(false);
            turnInHumanText.gameObject.SetActive(false);
            
            messageShowCount = 0;
        }

        public void TurnEnds()
        {
            int tempPlayerIndex;
            int nextPlayerIndex;
            tempPlayerIndex = currentPlayerIndex;
            while (true)
            {
                if (tempPlayerIndex != 4)
                {
                    nextPlayerIndex = tempPlayerIndex + 1;
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
                    if (playersType[currentPlayerIndex - 1].GetMazeVisibility() != 0)
                    {
                        playersType[currentPlayerIndex - 1].SetMazeVisibility(-1);
                    }
                    if (playersType[currentPlayerIndex - 1].GetDiceEffect() != 0)
                    {
                        playersType[currentPlayerIndex - 1].SetDiceEffect(-1);
                    }
                    if (playersType[currentPlayerIndex - 1].GetPositionVisibility() != 0)
                    {
                        playersType[currentPlayerIndex - 1].SetPositionVisibility(-1);
                    }
                    break;
                }
                else
                {
                    playersType[nextPlayerIndex - 1].SetTurnsPassed(-1);
                    if (playersType[nextPlayerIndex - 1].GetTurnsPassed() == 0)
                    {
                        playersSprite[nextPlayerIndex - 1].GetComponent<SpriteRenderer>().color = Color.white;
                        //si fuiste incapacitado, cuando dejas de estarlo se te quita el color azul
                    }
                    tempPlayerIndex = nextPlayerIndex;

                    if (playersType[tempPlayerIndex - 1].GetAttackCoolDown() != 0)
                    {
                        playersType[tempPlayerIndex - 1].SetAttackCoolDown(-1);
                    }
                    if (playersType[tempPlayerIndex - 1].GetSkillCoolDown() != 0)
                    {
                        playersType[tempPlayerIndex - 1].SetSkillCoolDown(-1);
                    }
                    if (playersType[currentPlayerIndex - 1].GetMazeVisibility() != 0)
                    {
                        playersType[currentPlayerIndex - 1].SetMazeVisibility(-1);
                    }
                    if (playersType[currentPlayerIndex - 1].GetDiceEffect() != 0)
                    {
                        playersType[currentPlayerIndex - 1].SetDiceEffect(-1);
                    }
                    if (playersType[currentPlayerIndex - 1].GetPositionVisibility() != 0)
                    {
                        playersType[currentPlayerIndex - 1].SetPositionVisibility(-1);
                    }
                    //los cambios en los cooldown va a ocurrir cuando te toca aunque se te salte en turno}
                    //introduzco el temp player index para que las otras funciones y clase q tocan el currentPlayerIndex no tengan algun problema en el update
                }
            }
            if (playersType[Instancia.currentPlayerIndex - 1].GetMazeVisibility() == 0)
            {
                MapTrapEffectImage.color = Color.white;
            }
            else
            {
                MapTrapEffectImage.color = Color.gray;
            }
            if (playersType[Instancia.currentPlayerIndex - 1].GetDiceEffect() == 0)
            {
                diceEffectImage.color = Color.white;
            }
            else
            {
                diceEffectImage.color = Color.gray;
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
            //esta funcion va a atacar al que yo intente acercarme con "movimiento valido"(nearF y nearC) asi nos deshacemos de los casos donde 
            //la casilla del currentPlayerIndex es adyacente a la de mas de un jugador;
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
                                playersSprite[i].GetComponent<SpriteRenderer>().color = Color.blue;
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

        private void FellInTrap(Laberinto laberinto)
        {
            int number = laberinto.Read(FilasColumnas[currentPlayerIndex - 1][0], FilasColumnas[currentPlayerIndex - 1][1]);

            if (number == 3 || number == 4 || number == 5 || number == 6 || number == 7)
            {
                int f = FilasColumnas[currentPlayerIndex - 1][0];
                int c = FilasColumnas[currentPlayerIndex - 1][1];

                messageShowCount = 0;
                if (playersType[currentPlayerIndex - 1].GetTrapInmunity() == 0)
                {
                    if (number == 3 && !Human[currentPlayerIndex - 1])
                    {
                        ChangeMessage("Has activado una trampa", trapText);
                        if (playersType[currentPlayerIndex - 1].GetCollectedShards() != 0)
                        {
                            playersType[currentPlayerIndex - 1].SetCollectedShards(-1);
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
                        ChangeMessage("Te perderas 2 turnos", underTrapEffectText);
                        playersSprite[currentPlayerIndex - 1].GetComponent<SpriteRenderer>().color = Color.blue;
                        playersType[currentPlayerIndex - 1].SetTurnsPassed(2);
                        SpawnMaze.SpawnTile(c * SpawnMaze.tileWidth, (Laberinto.ElLaberinto.GetSize() - f - 1) * SpawnMaze.tileWidth, UnactiveTrap);
                        laberinto.SetPosValue(f, c, 1);
                        diceNumber = 0;
                    }

                    else if (number == 5)
                    {
                        ChangeMessage("Has activado una trampa", trapText);// texto temporal
                        ChangeMessage("+3 turnos antes de poder atacar", underTrapEffectText);
                        playersType[currentPlayerIndex - 1].SetAttackCoolDown(3);
                        laberinto.SetPosValue(f, c, 1);
                        SpawnMaze.SpawnTile(c * SpawnMaze.tileWidth, (Laberinto.ElLaberinto.GetSize() - f - 1) * SpawnMaze.tileWidth, UnactiveTrap);
                    }
                    else if (number == 6)
                    {
                        ChangeMessage("Has activado una trampa", trapText);// texto temporal
                        ChangeMessage("+3 turnos:No podras ver el mapa", underTrapEffectText);
                        MapTrapEffectImage.color = Color.gray;
                        playersType[currentPlayerIndex - 1].SetMazeVisibility(3);
                        laberinto.SetPosValue(f, c, 1);
                        SpawnMaze.SpawnTile(c * SpawnMaze.tileWidth, (Laberinto.ElLaberinto.GetSize() - f - 1) * SpawnMaze.tileWidth, UnactiveTrap);
                    }
                    else if (number == 7)
                    {
                        ChangeMessage("Has activado una trampa", trapText);// texto temporal
                        ChangeMessage("+3 turnos:Tirada de dado se reduce a la mitad", underTrapEffectText);
                        diceEffectImage.color = Color.gray;
                        playersType[currentPlayerIndex - 1].SetDiceEffect(3);
                        diceNumber /= 2;
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
        }
        public void SkillButton()
        {
            playersType[currentPlayerIndex - 1].Skill();
            trapText.gameObject.SetActive(false);
            underTrapEffectText.gameObject.SetActive(false);
            messageShowCount = 0;
        }
        public void TurnInHuman()
        {
            playersSprite[currentPlayerIndex - 1].GetComponent<SpriteRenderer>().sprite = humanSprites[currentPlayerIndex - 1];
            playersType[currentPlayerIndex - 1].SetSkillCoolDown(int.MaxValue - playersType[currentPlayerIndex - 1].GetSkillCoolDown());
            //asi cuando la funcion lo sume internamente no se desborda de MaxValue
            turnInHumanText.gameObject.SetActive(true);
            Human[currentPlayerIndex - 1] = true;
        }

        public void PassTurnButton()
        {
            diceNumber = 0;
        }
        public void UseKeyButton()
        {
            playersType[currentPlayerIndex - 1].SetCollectedKeys(-1);
            Laberinto.ElLaberinto.SetPosValue(nearDoorF, nearDoorC, 1);
            SpawnMaze.SpawnTile(nearDoorC * SpawnMaze.tileWidth, (Laberinto.ElLaberinto.GetSize() - nearDoorF - 1) * SpawnMaze.tileWidth, OpenDoor);
            useKeyButton.gameObject.SetActive(false);
        }

        public void BreakWallButton()
        {
            Laberinto.ElLaberinto.SetPosValue(nearBrokenWallF, nearBrokenWallC, 1);
            SpawnMaze.SpawnTile(nearBrokenWallC * SpawnMaze.tileWidth, (Laberinto.ElLaberinto.GetSize() - nearBrokenWallF - 1) * SpawnMaze.tileWidth, Path);
            diceNumber = 0;
            //romper una pared consume lo que te queda del turno
            breakWallButton.gameObject.SetActive(false);
        }

        void Update()
        {
            var laberinto = Laberinto.ElLaberinto;
            ChangeMessage($"{diceNumber}", remainingMovesText);
            if (Human[currentPlayerIndex - 1] == false)
            {
                ChangeMessage($"{playersType[currentPlayerIndex - 1].GetCollectedShards()} Fragmentos de Alma", shardCollectionText);
            }
            else
            {
                shardCollectionText.gameObject.SetActive(false);
            }

            if (diceNumber == 0)
            {
                ChangeMessage("Presiona espacio para pasar el turno", changeTurnText);
                passTurnButton.gameObject.SetActive(false);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    TurnEnds();
                }
                for (int i = 0; i < playersType.Length; i++)
                {
                    if (cameras[i].gameObject.activeSelf && i != currentPlayerIndex - 1)
                    {
                        cameras[i].gameObject.SetActive(false);
                        cameras[currentPlayerIndex - 1].gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                changeTurnText.gameObject.SetActive(false);
                passTurnButton.gameObject.SetActive(true);
            }

            if (playersType[currentPlayerIndex - 1].GetSkillCoolDown() == 0 && diceNumber != 0)
            {
                skillButton.gameObject.SetActive(true);
                skillCoolDownText.gameObject.SetActive(false);
            }
            else
            {
                skillButton.gameObject.SetActive(false);
                if (!Human[currentPlayerIndex - 1] && diceNumber != 0)
                {
                    ChangeMessage($"Enfriamiento:{playersType[currentPlayerIndex - 1].GetSkillCoolDown()} turnos", skillCoolDownText);
                }
                else
                {
                    skillCoolDownText.gameObject.SetActive(false);
                }
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
            if (playersType[Instancia.currentPlayerIndex - 1].GetCollectedKeys() != 0)
            {
                keyOwnershipImage.color = Color.white;
            }
            else
            {
                keyOwnershipImage.color = Color.gray;
            }
            if (playersType[Instancia.currentPlayerIndex - 1].GetAttackCoolDown() == 0)
            {
                attackCoolDownImage.color = Color.white;
            }
            else
            {
                attackCoolDownImage.color = Color.gray;
            }
        }
    }
}
